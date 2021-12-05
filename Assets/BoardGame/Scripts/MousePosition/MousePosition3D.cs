using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{
	[SerializeField] private Camera _mainCamera;
	[SerializeField] private LayerMask _layerMask;

	private PreviewMessage _previewMessage;
	private PreviewPlayerName _previewPlayerName;

	private void Awake()
	{
		_previewMessage = Resources.FindObjectsOfTypeAll<PreviewMessage>()[0];
		_previewPlayerName = Resources.FindObjectsOfTypeAll<PreviewPlayerName>()[0];
	}

	private void Update()
	{
		Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
		{
			transform.position = raycastHit.point;
		}

		Vector3 tempPosition = Input.mousePosition;

		if (_previewMessage.gameObject.activeInHierarchy)
		{
			Vector2Int offset = new Vector2Int(1150, 400);
			tempPosition.x -= offset.x;
			tempPosition.y -= offset.y;
	
			tempPosition.x = Mathf.Clamp(tempPosition.x, -750, 350);
			tempPosition.y = Mathf.Clamp(tempPosition.y, -500, 375);

			_previewMessage.transform.localPosition = tempPosition;
		}
		else if (_previewPlayerName.gameObject.activeInHierarchy)
		{
			Vector2Int offset = new Vector2Int(808, 512);
			tempPosition.x -= offset.x;
			tempPosition.y -= offset.y;

			RectTransform rt = _previewPlayerName.GetComponent<RectTransform>();
			tempPosition.x = Mathf.Clamp
			(
				tempPosition.x, 
				-1150f - rt.rect.x * 2f,
				-350f - rt.rect.x * 2f
			);

			tempPosition.y = Mathf.Clamp(tempPosition.y, -1000, 500);

			_previewPlayerName.transform.localPosition = tempPosition;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
<<<<<<< HEAD
=======
		if (other.TryGetComponent(out Player player))
		{
			_previewPlayerName.gameObject.SetActive(true);
			TextMeshProUGUI playerName = _previewPlayerName.GetComponentInChildren<VariableText>().GetComponent<TextMeshProUGUI>();
			
			RectTransform rt = _previewPlayerName.GetComponent<RectTransform>();
			//rt.sizeDelta = new Vector2(player.Name.Length * 30, 50);			
			rt.SetSizeWithCurrentAnchors
			(
				RectTransform.Axis.Horizontal, 
				player.Name.Length * 30
			);

			playerName.text = player.Name;
			return;
		}
		
>>>>>>> dev
		if (other.TryGetComponent(out Point point) == false
			|| point.EffectType == EffectType.Normal)
		{
			return;
		}

		_previewMessage.transform.LookAt(_mainCamera.transform);

		Quaternion tempQuaternion = Quaternion.identity;
		tempQuaternion.x = 0.5f;
		_previewMessage.transform.rotation = tempQuaternion;

		_previewMessage.gameObject.SetActive(true);
<<<<<<< HEAD
		var text = _previewMessage.GetComponentInChildren<TextMeshProUGUI>();
		text.text = point.Message;
		//Vector3 tempPosition = Input.mousePosition.normalized;
		//_previewMessage.transform.position = tempPosition;
=======
		TextMeshProUGUI message = _previewMessage.GetComponentInChildren<VariableText>().GetComponent<TextMeshProUGUI>();
		message.text = point.Message;
>>>>>>> dev
	}

	private void OnTriggerExit(Collider other)
	{
<<<<<<< HEAD
=======
		_previewPlayerName.gameObject.SetActive(false);
>>>>>>> dev
		_previewMessage.gameObject.SetActive(false);
	}
}
