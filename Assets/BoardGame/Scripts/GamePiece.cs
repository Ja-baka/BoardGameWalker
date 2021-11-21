using UnityEngine;

public class GamePiece : MonoBehaviour
{
    private Vector3 _offset;
	private Centre _centre;

	private void OnEnable()
	{
		_centre = FindObjectOfType<Centre>();
		_offset = _centre.transform.position - transform.position;

		Debug.Log($"{gameObject} enable. Offset = {_offset}");
	}
}
