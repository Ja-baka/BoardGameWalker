using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour, IPointerClickHandler
{
	[HideInInspector] public EventHandler<DiceEventArgs> RolledEvent;

	private bool _isActualValue = false;
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

		_rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

		float randomX = UnityEngine.Random.Range(-360f, 360f);
		float randomY = UnityEngine.Random.Range(-360f, 360f);
		float randomZ = UnityEngine.Random.Range(-360f, 360f);

		_rigidbody.AddTorque(randomX, randomY, randomZ);
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
