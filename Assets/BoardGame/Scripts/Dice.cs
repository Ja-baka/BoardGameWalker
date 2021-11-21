using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour, IPointerClickHandler
{
	public int Value { get; private set; } = 0;

	[SerializeField] private float _jumpForce = 10f;
	
	private DiceSide[] _sides = new DiceSide[6];
	private Rigidbody _rigidbody;
	private bool _isThrown = false;
    private bool _isActualValue = false;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_sides = GetComponentsInChildren<DiceSide>();
	}

    private void FixedUpdate()
    {
		_isThrown = !_rigidbody.IsSleeping();
        
        if (_isThrown == false
            && _isActualValue == false)
        {
			DiceSideCheck();

			if (Value == 0)
			{
				Rolling();
				return;
			}

			_isActualValue = true;
            Debug.Log($"Value = {Value}");
			
        }
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		Rolling();
	}

	private void Rolling()
	{
		if (_isThrown == true)
		{
			return;
		}

		_isActualValue = false;

		_rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

		float randomX = Random.Range(-360f, 360f);
		float randomY = Random.Range(-360f, 360f);
		float randomZ = Random.Range(-360f, 360f);

		_rigidbody.AddTorque(randomX, randomY, randomZ);
	}

	private void DiceSideCheck()
    {
		Value = 0;
		foreach (DiceSide side in _sides)
		{
			if (side.IsGrounded)
			{
				Value = side.SideValue;
				Debug.Log($"выпало число {Value}");
				return;
			}
		}
    }
}
