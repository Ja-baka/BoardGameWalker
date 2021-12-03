using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePointer : MonoBehaviour
{
	[SerializeField] private Camera _mainCamera;

	private Dice _dice;
	private PlayersMover _playersMover;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private Transform _target;

	private void Awake()
	{
		_dice = FindObjectOfType<Dice>();
		_playersMover = Resources.FindObjectsOfTypeAll<PlayersMover>()[0];
	}

	private void Start()
	{
		_animator = GetComponentInChildren<Animator>();
		_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		_target = FindObjectOfType<Dice>().transform;
	}

	private void OnEnable()
	{
		_dice.ThrownEvent += HidePointer;
		_playersMover.FinishMoveEvent += ShowPointer;
	}
	private void OnDisable()
	{
		if (_dice != null
			&& _playersMover != null)
		{
			_dice.ThrownEvent -= HidePointer;
			_playersMover.FinishMoveEvent -= ShowPointer;
		}
	}

	private void ShowPointer(object sender, PlayerEvent e)
	{
		_animator.enabled = true;
		_spriteRenderer.enabled = true;
	}
	private void HidePointer(object sender, DiceEventArgs e)
	{
		_animator.enabled = false;
		_spriteRenderer.enabled = false;
	}

	private void Update()
	{
		transform.position = _target.position + Vector3.up;
		transform.LookAt(_mainCamera.transform);
	}
}
