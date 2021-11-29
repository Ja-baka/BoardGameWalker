using System.Collections;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Player _activePlayer;

	private void OnEnable()
	{
		_text = GetComponentInChildren<TextMeshProUGUI>();
		PlayersMover mover = FindObjectOfType<PlayersMover>();
		_activePlayer = mover.ActivePlayer;
		_text.text = $"Ход игрока {_activePlayer.Name}!";
	}
}
