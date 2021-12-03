//using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour, IPointerClickHandler
{
	[HideInInspector] public System.EventHandler<DiceEventArgs> RolledEvent;
	[HideInInspector] public System.EventHandler<DiceEventArgs> ThrownEvent;

	private bool _isActualValue = true;
	private Rigidbody _rigidbody;

	private const float _jumpForce = 10f;

	public void OnPointerClick(PointerEventData eventData)
	{
		Rolling();
	}

    // DEBUG: чит
	private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
			RolledEvent?.Invoke(this, new DiceEventArgs(1));
        }
		else if (Input.GetKey(KeyCode.Alpha2))
        {
			RolledEvent?.Invoke(this, new DiceEventArgs(2));
        }
		else if (Input.GetKey(KeyCode.Alpha3))
        {
			RolledEvent?.Invoke(this, new DiceEventArgs(3));
        }
		else if (Input.GetKey(KeyCode.Alpha4))
        {
			RolledEvent?.Invoke(this, new DiceEventArgs(4));
        }
		else if (Input.GetKey(KeyCode.Alpha5))
        {
			RolledEvent?.Invoke(this, new DiceEventArgs(5));
        }
		else if (Input.GetKey(KeyCode.Alpha6))
        {
			RolledEvent?.Invoke(this, new DiceEventArgs(6));
        }
    }
    // DEBUG: чит

    private void Rolling()
    {
        if (_rigidbody.IsSleeping() == false)
        {
            return;
        }

		ThrownEvent?.Invoke(this, new DiceEventArgs(0));
		_isActualValue = false;

        SetRandomForce();
        SetRandomRotation();
    }

    private void SetRandomForce()
    {
        Vector3 randomDirection = GenerateRandomDirection();
        Vector3 jumpDirection = Vector3.up + (randomDirection / 2);
        _rigidbody.AddForce
        (
            jumpDirection * _jumpForce,
            ForceMode.Impulse
        );
    }

    private void SetRandomRotation()
    {
		Vector3 randomVector = new Vector3
		(
			Random.Range(-180f, 180f),
			Random.Range(-180f, 180f),
			Random.Range(-180f, 180f)
		);
        _rigidbody.AddTorque
		(
			randomVector.x,
			randomVector.y,
			randomVector.z
		);
    }

    private static Vector3 GenerateRandomDirection()
	{
		return UnityEngine.Random.Range(1, 5) switch
		{
			1 => Vector3.forward,
			2 => Vector3.back,
			3 => Vector3.right,
			4 => Vector3.left,
			_ => Vector3.zero,
		};
	}

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

    private void FixedUpdate()
    {
        if (_rigidbody.IsSleeping() == true
			&& _isActualValue == false)
        {
			DiceSideCheck();
        }
    }

	private void DiceSideCheck()
    {
		foreach (DiceSide side in GetComponentsInChildren<DiceSide>())
		{
			if (side.IsGrounded)
			{
				RolledEvent?.Invoke(this, new DiceEventArgs(side.SideValue));
				_isActualValue = true;
				return;
			}
		}

		Rolling();
	}
}
