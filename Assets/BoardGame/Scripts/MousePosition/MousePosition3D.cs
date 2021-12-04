using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{
	[SerializeField] private Camera _mainCamera;
	[SerializeField] private LayerMask _layerMask;

	private PreviewMessage _previewMessage;

	private void Awake()
	{
		_previewMessage = Resources.FindObjectsOfTypeAll<PreviewMessage>()[0];
	}

	private void Update()
	{
		Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
		{
			transform.position = raycastHit.point;
		}

		if (_previewMessage.gameObject.activeInHierarchy == false)
		{
			return;
		}

		Vector3 tempPosition = Input.mousePosition;
		tempPosition.x -= 1150;
		tempPosition.y -= 400;

		tempPosition.x = Mathf.Clamp(tempPosition.x, -750, 350);
		tempPosition.y = Mathf.Clamp(tempPosition.y, -500, 375);

		_previewMessage.transform.localPosition = tempPosition;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Point point) == false
			|| point.EffectType == EffectType.Normal)
		{
			return;
		}

		_previewMessage.gameObject.SetActive(true);
		TextMeshProUGUI text = _previewMessage.GetComponentInChildren<VariableText>().GetComponent<TextMeshProUGUI>();
		text.text = point.Message;
	}

	private void OnTriggerExit(Collider other)
	{
		_previewMessage.gameObject.SetActive(false);
	}
}
