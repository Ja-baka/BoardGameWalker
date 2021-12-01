using UnityEngine;

public class Player : MonoBehaviour
{
	public Vector3 Offset => _offset;
	public Color Color;
	public string Name = "Имя не задано";

	// TODO: вот бы инкапсулировать через события
	[HideInInspector] public int CurrentPoint = 0;

	private Vector3 _offset;

	private void OnEnable()
	{
		_offset = transform.localPosition;
		CurrentPoint = 0;
	}
}
