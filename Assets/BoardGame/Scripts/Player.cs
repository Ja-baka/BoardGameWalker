using UnityEngine;

public class Player : MonoBehaviour
{
	public Vector3 Offset => _offset;
	public Color Color;
	public string Name = "Имя не задано";
	public int MovesCount = 1;

	[HideInInspector] public int CurrentPoint = 0;

	private Vector3 _offset;

	private void Awake()
	{
		_offset = transform.localPosition;
	}

	private void OnEnable()
	{
		CurrentPoint = 0;
	}
}
