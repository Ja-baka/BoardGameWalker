using TMPro;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{
	[SerializeField] private Camera _mainCamera;
	[SerializeField] private LayerMask _layerMask;

	private PreviewMessage _pointMessage;
	private PreviewPlayerName _playerName;

	private void Awake()
	{
		_pointMessage = Resources.FindObjectsOfTypeAll<PreviewMessage>()[0];
		_playerName = Resources.FindObjectsOfTypeAll<PreviewPlayerName>()[0];
	}

	private void Update()
	{
		Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, _layerMask))
		{
			transform.position = raycastHit.point;
		}

		if (_pointMessage.gameObject.activeInHierarchy)
		{
			MovePointMessage();
		}
		else if (_playerName.gameObject.activeInHierarchy)
		{
			MovePlayerName();
		}
	}

	private void MovePlayerName()
	{
		Vector3 tempPosition = Input.mousePosition;
		int scaleX = Screen.width / 1920;
		int scaleY = Screen.height / 1080;
		Vector2Int offset = new Vector2Int(scaleX * 808, scaleY * 512);
		tempPosition.x -= offset.x;
		tempPosition.y -= offset.y;

		RectTransform rectTransform = _playerName
			.GetComponent<RectTransform>();
		tempPosition.x = Mathf.Clamp
		(
			tempPosition.x,
			scaleX * -1150f - rectTransform.rect.x * 2f,
			scaleY * -350f - rectTransform.rect.x * 2f
		);

		tempPosition.y = Mathf.Clamp(tempPosition.y, scaleX * -1000, scaleY * 500);

		_playerName.transform.localPosition = tempPosition;
	}

	private void MovePointMessage()
	{
		int scaleX = Screen.width / 1920;
		int scaleY = Screen.height / 1080;
		Vector3 tempPosition = Input.mousePosition;
		Vector2Int offset = new Vector2Int(scaleX * 1150, scaleY * 400);
		tempPosition.x -= offset.x;
		tempPosition.y -= offset.y;

		tempPosition.x = Mathf.Clamp(tempPosition.x, scaleX * -750, scaleY * 350);
		tempPosition.y = Mathf.Clamp(tempPosition.y, scaleX * -500, scaleY * 375);

		_pointMessage.transform.localPosition = tempPosition;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Point point)
			&& point.EffectType != EffectType.Normal)
		{
			PreviewPointMessage(point);
		}
		else if (other.TryGetComponent(out Player player))
		{
			PreviewPlayerName(player);
		}
	}

	private void PreviewPointMessage(Point point)
	{
		_pointMessage.gameObject.SetActive(true);

		TextMeshProUGUI message = _pointMessage
			.GetComponentInChildren<VariableText>().GetComponent<TextMeshProUGUI>();
		message.text = point.Message;
	}

	private void PreviewPlayerName(Player player)
	{
		_playerName.gameObject.SetActive(true);
		TextMeshProUGUI playerName = _playerName
			.GetComponentInChildren<VariableText>().GetComponent<TextMeshProUGUI>();

		RectTransform rt = _playerName.GetComponent<RectTransform>();
		rt.SetSizeWithCurrentAnchors
		(
			RectTransform.Axis.Horizontal,
			player.Name.Length * 30
		);

		playerName.text = player.Name;
	}

	private void OnTriggerExit(Collider other)
	{
		_playerName.gameObject.SetActive(false);
		_pointMessage.gameObject.SetActive(false);
	}
}