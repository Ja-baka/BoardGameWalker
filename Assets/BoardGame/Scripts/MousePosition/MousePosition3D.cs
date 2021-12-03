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
	}

	private void OnTriggerEnter(Collider other)
	{
		print("enter");
		if (other.TryGetComponent(out Point point) == false
			|| point.EffectType == EffectType.Normal)
		{
			return;
		}

		_previewMessage.gameObject.SetActive(true);
		var text = _previewMessage.GetComponentInChildren<TextMeshProUGUI>();
		text.text = point.Message;
		//_previewMessage.transform.position = Input.mousePosition;
	}



	private void OnTriggerExit(Collider other)
	{
		print("exit");
		_previewMessage.gameObject.SetActive(false);
	}
}
