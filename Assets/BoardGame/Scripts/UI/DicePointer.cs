using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePointer : MonoBehaviour
{
	[SerializeField] private Image _pointerImage;
	[SerializeField] private Camera _camera;
	
	private Transform _target;
	private Vector2 _position;
	private Vector3 _tempPosition = new Vector3();

	private void Start()
	{
		_target = FindObjectOfType<Dice>().transform;
	}

	private void OnEnable()
	{
        Dice dice = _target.GetComponent<Dice>();
        dice.ThrownEvent += HidePointer;
		dice.RolledEvent += ShowPointer;
	}
	private void OnDisable()
	{
        Dice dice = _target.GetComponent<Dice>();
		dice.ThrownEvent -= HidePointer;
		dice.RolledEvent -= ShowPointer;
	}

	private void ShowPointer(object sender, DiceEventArgs e)
	{
		gameObject.SetActive(true);
	}
	private void HidePointer(object sender, DiceEventArgs e)
	{
		gameObject.SetActive(false);
	}

	private void Update()
	{
		//transform.position = _camera.WorldToScreenPoint(_target.position);
		transform.position = _target.position + Vector3.up;
		// _position.x = Mathf.Clamp(_position.x, 0f, Screen.width);
		// _position.y = Mathf.Clamp(_position.y, 0f, Screen.height);
		//_pointerImage.transform.position = _position;
	}
}
