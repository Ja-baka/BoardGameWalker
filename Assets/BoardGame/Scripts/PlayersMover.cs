using System;
using System.Collections;
using UnityEngine;

public class PlayersMover : MonoBehaviour
{
	[SerializeField] private float _speed = 1f;

	private Player[] _players;
	private Player _activePlayer;
	private Point[] _points;
	private Coroutine _movingCoroutine = null;
	private Transform _target;

	private readonly WaitForSeconds _waitforSecond = new WaitForSeconds(1f);

	private void OnEnable()
	{
		Dice dice = FindObjectOfType<Dice>();
		dice.RolledEvent += StartMoving;

		Transform path = FindObjectOfType<Path>().transform;
		_points = path.GetComponentsInChildren<Point>(); ;


		_players = FindObjectsOfType<Player>();
		_activePlayer = _players[0];
	}

	private void StartMoving(object sender, DiceEventArgs e)
	{
		if (_movingCoroutine == null)
		{
			_movingCoroutine = StartCoroutine(MovmentCoroutine(e.Value));
		}
	}

	private IEnumerator MovmentCoroutine(int DiceValue)
	{
		for (int i = _activePlayer.CurrentPoint;
			i < _activePlayer.CurrentPoint + DiceValue;
			i++)
		{
			MoveToCurrentPoint();
			yield return SwitchToNextPoint();
		}
	}

	private void MoveToCurrentPoint()
	{
		_target = _points[_activePlayer.CurrentPoint].transform;
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
		_activePlayer.CurrentPoint++;

		if (_activePlayer.CurrentPoint >= _points.Length)
		{
			StopCoroutine(_movingCoroutine);
			_movingCoroutine = null;
		}
	}
}
