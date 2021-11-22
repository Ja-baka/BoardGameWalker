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

		Vector3 RandomSite = Vector3.zero;

		switch (UnityEngine.Random.Range(1, 5))
		{
			case 1:
				RandomSite = Vector3.forward;
				break;
			case 2:
				RandomSite = Vector3.back;
				break;
			case 3:
				RandomSite = Vector3.right;
				break;
			case 4:
				RandomSite = Vector3.left;
				break;
		}

		_rigidbody.AddForce((Vector3.up + (RandomSite / 2)) * _jumpForce, ForceMode.Impulse);

		float randomX = UnityEngine.Random.Range(-180, 180);
		float randomY = UnityEngine.Random.Range(-180, 180);
		float randomZ = UnityEngine.Random.Range(-180, 180);

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
