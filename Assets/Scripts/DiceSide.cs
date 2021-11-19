using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
	public int SideValue { get => _sideValue; }
	public bool IsGrounded { get => _isGrounded; /*set => _isGrounded = value;*/ }

	[SerializeField] private int _sideValue;
	
	private bool _isGrounded;

	private void OnValidate()
	{
		if (_sideValue < 1
			|| _sideValue > 6)
		{
			_sideValue = 1;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		_isGrounded = other.TryGetComponent(out Ground _);
	}

	private void OnTriggerExit(Collider other)
	{
		_isGrounded = false;
	}
}
