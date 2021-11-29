using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	private Scrollbar _scrollbar;
	private TMP_InputField[] _inputFields;
	private Player[] _players;
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
			if (_inputFields[i].interactable == false)
			{
				continue;
			}
			string input = _inputFields[i].textComponent.text;
			_players[i].Name = input != string.Empty
				? input 
				: $"Игрок {i + 1}";
		}
	}

	private void HideMainMenu()
	{
		gameObject.SetActive(false);
	}

	private void ShowPlayers()
	{
		for (int i = 0; i < _players.Length; i++)
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

		List<Player> tempPlayersList = new List<Player>();
		foreach (Player player1 in Resources.FindObjectsOfTypeAll<Player>())
		{
			if (EditorUtility.IsPersistent(player1) == false)
			{
				tempPlayersList.Add(player1);
			}
		}
		_players = tempPlayersList.ToArray();

		_playersMover = Resources.FindObjectsOfTypeAll<PlayersMover>()[0];
	}
}
