using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour, IPointerClickHandler
{
	[HideInInspector] public EventHandler<DiceEventArgs> RolledEvent;

	private bool _isActualValue = true;
	private Rigidbody _rigidbody;

	private const float _jumpForce = 10f;

	public void OnPointerClick(PointerEventData eventData)
	{
		Rolling();
	}

	private void Rolling()
	{
		if (_rigidbody.IsSleeping() == false)
		{
			return;
		}

		_isActualValue = false;

		Vector3 randomDirection = GenerateRandomDirection();
		Vector3 jumpDirection = Vector3.up + (randomDirection / 2);
		_rigidbody.AddForce
		(
			jumpDirection * _jumpForce, 
			ForceMode.Impulse
		);

		float randomX = UnityEngine.Random.Range(-180, 180);
		float randomY = UnityEngine.Random.Range(-180, 180);
		float randomZ = UnityEngine.Random.Range(-180, 180);

		_rigidbody.AddTorque(randomX, randomY, randomZ);
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
