using System;
using System.Collections;
using UnityEngine;

// TODO: неявный гейм менеджер. разбить
public class PlayersMover : MonoBehaviour
{
	public Player ActivePlayer => _activePlayer;

	private Player[] _players;
	private Player _activePlayer;
	private int _IndexActivePlayer;
	private Point[] _points;
	private Coroutine _movingCoroutine = null;
	private Vector3 _targetPoint;
	private int _targetPointIndex;
	private int _endPointIndex;

	private const float _speed = 5f;
	private readonly WaitForSeconds _waitforSecond = new WaitForSeconds(0.1f);

	private void OnEnable()
	{
		var dice = FindObjectOfType<Dice>();
		dice.RolledEvent += StartMoving;

		Transform path = FindObjectOfType<Path>().transform;
		_points = path.GetComponentsInChildren<Point>(); ;

		_players = FindObjectsOfType<Player>();
		_IndexActivePlayer = 0;
		_activePlayer = _players[_IndexActivePlayer];
	}

	private void OnDisable()
	{
		Dice dice = FindObjectOfType<Dice>();
		if (dice != null)
		{
			dice.RolledEvent -= StartMoving;
		}
	}

	private void StartMoving(object sender, DiceEventArgs e)
	{
		if (_movingCoroutine == null)
		{
			_movingCoroutine = StartCoroutine(MovmentCoroutine(e.Value));
		}
	}

	private IEnumerator MovmentCoroutine(int diceValue)
	{
		_targetPointIndex = _activePlayer.CurrentPoint + 1;
		_endPointIndex = _activePlayer.CurrentPoint + diceValue;

		while (_targetPointIndex <= _endPointIndex
			&& _targetPointIndex < _points.Length)
		{
			MoveToCurrentPoint();
			yield return SwitchToNextPoint();
		}
	}

	private void MoveToCurrentPoint()
	{
		_targetPoint = _points[_targetPointIndex].transform.position;
		_activePlayer.transform.position = Vector3.MoveTowards
		(
			_activePlayer.transform.position,
			_targetPoint + _activePlayer.Offset,
			_speed * Time.deltaTime
		);
	}

	private IEnumerator SwitchToNextPoint()
	{
		if (_activePlayer.transform.position != _targetPoint + _activePlayer.Offset)
		{
			yield break;
		}

		yield return _waitforSecond;
		_activePlayer.CurrentPoint++;
		_targetPointIndex++;

		if (_activePlayer.CurrentPoint == _points.Length - 1)
		{
			FinishMove();
			FinishGame();
			yield break;
		}

		if (_activePlayer.CurrentPoint == _endPointIndex)
		{
			Message message = Resources.FindObjectsOfTypeAll<Message>()[0];
			message.gameObject.SetActive(true);
			FinishMove();
		}
	}

	private void FinishMove()
	{
		StopCoroutine(_movingCoroutine);
		_movingCoroutine = null;

		Point currentPoint = _points[_activePlayer.CurrentPoint];
		EffectType pointType = currentPoint.EffectType;
		
		// TODO: ShowMesage(currentPoint.Message);
		if (pointType == EffectType.Normal)
		{
			SwichActivePlayer();
		}
		else if (pointType == EffectType.PlusMove)
		{
			// TODO: _activePlayer.Moves += currentPoint.EffectValue;
		}
		else if (pointType == EffectType.MinusMove)
		{
			// TODO: _activePlayer.Moves -= currentPoint.EffectValue;
		}
		else if (pointType == EffectType.PlusPoints)
		{ 
			StartMoving(this, new DiceEventArgs(currentPoint.EffectValue));
		}
		else if (pointType == EffectType.MinusPoints)
		{
			//StartMoving(this, new DiceEventArgs(currentPoint.EffectValue));
		}
	}

	private void FinishGame()
	{
		foreach (Player player in _players)
		{
			player.gameObject.SetActive(false);
			player.transform.position = _points[0].transform.position + player.Offset;
		}


		MainMenu mainMenu = Resources.FindObjectsOfTypeAll<MainMenu>()[0];
		mainMenu.gameObject.SetActive(true);
	}

	private void SwichActivePlayer()
	{
		_IndexActivePlayer++;
		_IndexActivePlayer = _IndexActivePlayer < _players.Length 
			? _IndexActivePlayer 
			: 0;

		_activePlayer = _players[_IndexActivePlayer];
	}
}
