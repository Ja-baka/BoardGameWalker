using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	private Scrollbar _scrollbar;
	private TMP_InputField[] _inputFields;
	private List<Player> _players;
	private PlayersMover _playersMover;

	public void SetInputFieldInteractable()
	{
		for (int i = 0; i < _inputFields.Length; i++)
		{
			bool interactable = _scrollbar.value >= i * 0.25f;
			_inputFields[i].interactable = interactable;
		}
	}

	public void StartPlay()
	{
		GetPlayersNames();
		HideMainMenu();
		ShowPlayers();
	}

	private void GetPlayersNames()
	{
		for (int i = 0; i < _inputFields.Length; i++)
		{
			if (_inputFields[i].interactable == true)
			{
				_players[i].Name
					= _inputFields[i].textComponent.text;
			}
		}
	}

	private void HideMainMenu()
	{
		gameObject.SetActive(false);
	}

	private void ShowPlayers()
	{
		for (int i = 0; i < _players.Count; i++)
		{
			bool isActive = _inputFields[i].interactable;
			_players[i].gameObject.SetActive(isActive);
		}

		_playersMover.gameObject.SetActive(true);
	}

	private void Start()
	{
		_scrollbar = GetComponentInChildren<Scrollbar>();
		_inputFields = GetComponentsInChildren<TMP_InputField>();

		Player[] playersArray = Resources.FindObjectsOfTypeAll<Player>();
		_players = new List<Player>(playersArray);
		List<Player> tempPlayerList = new List<Player>(playersArray);

		foreach (Player player in _players)
		{
			if (EditorUtility.IsPersistent(player) == true)
			{
				tempPlayerList.Remove(player);
			}
		}
		_players = tempPlayerList;

		_playersMover = Resources.FindObjectsOfTypeAll<PlayersMover>()[0];
	}
}
