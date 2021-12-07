using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{
	[SerializeField] private Camera _mainCamera;
	[SerializeField] private LayerMask _layerMask;

	private PreviewMessage _message;
	private PreviewPlayerName _playerName;

	private void Awake()
	{
		_message = Resources.FindObjectsOfTypeAll<PreviewMessage>()[0];
		_playerName = Resources.FindObjectsOfTypeAll<PreviewPlayerName>()[0];
	}

	private void Update()
	{
		Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
		{
			transform.position = raycastHit.point;
		}

		Vector3 tempPosition = Input.mousePosition;

		if (_message.gameObject.activeInHierarchy)
		{
			Vector2Int offset = new Vector2Int(1150, 400);
			tempPosition.x -= offset.x;
			tempPosition.y -= offset.y;
	
			tempPosition.x = Mathf.Clamp(tempPosition.x, -750, 350);
			tempPosition.y = Mathf.Clamp(tempPosition.y, -500, 375);

			_message.transform.localPosition = tempPosition;
		}
		else if (_playerName.gameObject.activeInHierarchy)
		{
			Vector2Int offset = new Vector2Int(808, 512);
			tempPosition.x -= offset.x;
			tempPosition.y -= offset.y;

			RectTransform rt = _playerName.GetComponent<RectTransform>();
			tempPosition.x = Mathf.Clamp
			(
				tempPosition.x, 
				-1150f - rt.rect.x * 2f,
				-350f - rt.rect.x * 2f
			);

			tempPosition.y = Mathf.Clamp(tempPosition.y, -1000, 500);

			_playerName.transform.localPosition = tempPosition;
		}
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
		_message.gameObject.SetActive(true);

		TextMeshProUGUI message = _message
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
		_message.gameObject.SetActive(false);
	}
}
