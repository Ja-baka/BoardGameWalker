using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiecesMover : MonoBehaviour
{
	public bool IsMoving = false;

	[SerializeField] private Transform _path;
	[SerializeField] private float _speed;

	private Transform[] _points;
	private Transform _target;
	private int _indexCurrentPoint;
	private WaitForSeconds _waitforSecond = new WaitForSeconds(0.1f);
	private Coroutine _movingCoroutine = null;
	private Dice _dice;
	private int _countOfPassedPoints;

	private void Start()
	{
		_dice = FindObjectOfType<Dice>();
		_points = new Transform[_path.childCount];

		for (int i = 0; i < _path.childCount; i++)
		{
			_points[i] = _path.GetChild(i);
		}

		StartMoving();
	}

	private void StartMoving()
	{
		if (_movingCoroutine == null)
		{
			_movingCoroutine = StartCoroutine(MovmentCoroutine());
		}
	}

	private IEnumerator MovmentCoroutine()
	{
		while (true)
		{
			MoveToCurrentPoint();
			yield return SwitchToNextPoint();
		}
	}

	private void MoveToCurrentPoint()
	{
		_target = _points[_indexCurrentPoint];
		transform.position = Vector3.MoveTowards
		(
			transform.position,
			_target.position,
			_speed * Time.deltaTime
		);
	}

	private IEnumerator SwitchToNextPoint()
	{
		if (transform.position != _target.position)
		{
			yield break;
		}

		yield return _waitforSecond;
		_indexCurrentPoint++;
		_countOfPassedPoints++;

		if (_indexCurrentPoint >= _points.Length)
		{
			StopCoroutine(_movingCoroutine);
			_movingCoroutine = null;
		}
		else if (_dice.Value == _countOfPassedPoints)
		{
			yield return new WaitUntil(ContinueMoving);
			_countOfPassedPoints = 0;

		}
	}

	public bool ContinueMoving()
	{
		return IsMoving;
	}
}
