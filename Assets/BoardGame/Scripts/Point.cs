using UnityEngine;
using TMPro;

public enum PointType
{
	Normal,
	Positive,
	Negative
}

public class Point : MonoBehaviour
{
	public TextMeshPro Text;

	[SerializeField] private PointType _type;

	private void OnEnable()
	{
		Text = GetComponentInChildren<TextMeshPro>();
	}

	private void OnValidate()
	{
		if (_type == PointType.Normal)
		{
			gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = Color.yellow;
		}
		else if (_type == PointType.Positive)
		{
			gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
		}
		else if (_type == PointType.Negative)
		{
			gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
		}
	}
}
