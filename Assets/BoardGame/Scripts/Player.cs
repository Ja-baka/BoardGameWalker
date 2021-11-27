using UnityEngine;

public class Player : MonoBehaviour
{
	public Vector3 Offset;

	public Color Color;
	public string Name;

	[HideInInspector] public int CurrentPoint = 0;

	private void OnEnable()
	{
		CurrentPoint = 0;
	}
}
