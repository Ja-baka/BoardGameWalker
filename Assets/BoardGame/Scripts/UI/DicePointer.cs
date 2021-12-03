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
	private SpriteRenderer _spriteRenderer;
	private Animator _animator;

	private void Start()
	{
		_animator = GetComponentInChildren<Animator>();
		_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		_target = FindObjectOfType<Dice>().transform;
		_animator.enabled = true;
		_spriteRenderer.enabled = true;
	}

	private void OnEnable()
	{
        Dice dice = FindObjectOfType<Dice>();
        dice.ThrownEvent += HidePointer;
		dice.RolledEvent += ShowPointer;
	}
	private void OnDisable()
	{
        Dice dice = FindObjectOfType<Dice>();
		if (dice != null)
        {
			dice.ThrownEvent -= HidePointer;
			dice.RolledEvent -= ShowPointer;
        }
	}

	private void ShowPointer(object sender, DiceEventArgs e)
	{
		Debug.Log("RolledEvent");
		_animator.enabled = true;
		_spriteRenderer.enabled = true;
		//gameObject.SetActive(true);
	}
	private void HidePointer(object sender, DiceEventArgs e)
	{
		Debug.Log("ThrownEvent");
		_animator.enabled = false;
		_spriteRenderer.enabled = false;
		//gameObject.SetActive(false);
	}

	private void Update()
	{
		transform.position = _target.position + Vector3.up;
	}
}
