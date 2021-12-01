using System;
using System.Collections;
using UnityEngine;

// TODO: неявный гейм менеджер. разбить
public class PlayersMover : MonoBehaviour
{
	public Player NextPlayer => _players[GetNextPlayerIndex()];

	private Player[] _players;
	private Player _activePlayer;
	private Point[] _points;
	private Coroutine _movingCoroutine = null;
	private Vector3 _targetPoint;
	private int _indexOfTargetPoint = 0;
	private int _endPointIndex;
	private int _indexOfActivePlayer;
	private const float _speed = 5f;
	private readonly WaitForSeconds _waitforMiliSecond = new WaitForSeconds(0.1f);

	private void OnEnable()
	{
		Dice dice = FindObjectOfType<Dice>();
		dice.RolledEvent += StartMoving;

		Transform path = FindObjectOfType<Path>().transform;
		_points = path.GetComponentsInChildren<Point>();

		_players = FindObjectsOfType<Player>();
		_indexOfActivePlayer = 0;
		_activePlayer = _players[_indexOfActivePlayer];
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
		_movingCoroutine ??= StartCoroutine(MovmentCoroutine(e.Value));
	}

	private IEnumerator MovmentCoroutine(int diceValue)
	{
		int direction = Mathf.Abs(diceValue) / diceValue;
		_indexOfTargetPoint = _activePlayer.CurrentPoint + direction;
		_endPointIndex = _activePlayer.CurrentPoint + diceValue;

		bool isFrontMovement = diceValue > 0;

		while ((isFrontMovement == true
				&& _indexOfTargetPoint <= _endPointIndex
				&& _indexOfTargetPoint < _points.Length)
			|| (isFrontMovement == false
				&& _indexOfTargetPoint >= _endPointIndex
				&& _indexOfTargetPoint > 0))
		{
			MoveToCurrentPoint();
			yield return SwitchToNextPoint();
		}
	}

	private void MoveToCurrentPoint()
	{
		_targetPoint = _points[_indexOfTargetPoint].transform.position;
		_activePlayer.transform.position = Vector3.MoveTowards
		(
			_activePlayer.transform.position,
			_targetPoint + _activePlayer.Offset,
			_speed * Time.deltaTime
		);
	}

	private IEnumerator SwitchToNextPoint()
	{
		if (_activePlayer.transform.position 
			!= _targetPoint + _activePlayer.Offset)
		{
			yield break;
		}

		yield return _waitforMiliSecond;
		bool isFrontMove = _indexOfTargetPoint 
			> _activePlayer.CurrentPoint;

		if (isFrontMove)
		{
			_indexOfTargetPoint++;
			_activePlayer.CurrentPoint++;
		}
		else
		{
			_indexOfTargetPoint--;
			_activePlayer.CurrentPoint--;
		}

		// TODO: при этих условиях выходить из цикла
		if (_activePlayer.CurrentPoint == _points.Length - 1)
		{
			FinishMove();
			FinishGame();
			yield break;
		}

		if (_activePlayer.CurrentPoint == _endPointIndex)
		{
			FinishMove();
		}
	}

	private void FinishMove()
	{
		StopCoroutine(_movingCoroutine);
		_movingCoroutine = null;

		Point currentPoint = _points[_activePlayer.CurrentPoint];
		EffectType pointType = currentPoint.EffectType;
		
		MessageMenu messageMenu 
			= Resources.FindObjectsOfTypeAll<MessageMenu>()[0];
		messageMenu.gameObject.SetActive(true);

		if (pointType == EffectType.Normal)
		{
			messageMenu.ShowMessage($"Ход игрока {NextPlayer.Name}", currentPoint.Background);
			SwichActivePlayer();
			return;
		}
		else
		{ 
			messageMenu.ShowMessage(currentPoint.Message, currentPoint.Background);
		}

		if (pointType == EffectType.MoveCount)
		{
			if (currentPoint.EffectValue == 1)
			{
				Debug.Log("дополнительный ход");
			}
			else
			{
				Debug.Log("пропуск хода");
			}
			SwichActivePlayer();
		}
		else if (pointType == EffectType.Position)
		{
			StartMoving(this, new DiceEventArgs(currentPoint.EffectValue));
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
		_indexOfActivePlayer = GetNextPlayerIndex();
		_activePlayer = _players[_indexOfActivePlayer];
	}

	private int GetNextPlayerIndex()
	{
		int indexofNextPlayer = _indexOfActivePlayer + 1;

		indexofNextPlayer = indexofNextPlayer < _players.Length
			? indexofNextPlayer
			: 0;

		return indexofNextPlayer;
	}
}