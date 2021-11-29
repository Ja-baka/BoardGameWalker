using UnityEngine;

public class Player : MonoBehaviour
{
	public Color Color;
	public string Name = "��� �� ������";
	public Vector3 Offset => _offset;

	// TODO: ��� �� ��������������� ����� �������
	[HideInInspector] public int CurrentPoint = 0;

	private Vector3 _offset;

	private void OnEnable()
	{
		_offset = transform.localPosition;
		CurrentPoint = 0;
	}
}
