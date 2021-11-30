using System.Collections;
using UnityEngine;
using TMPro;

public class MessageMenu : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Player _nextPlayer;

	public void ShowMessage(string message)
	{
		_text = GetComponentInChildren<TextMeshProUGUI>();
		PlayersMover mover = FindObjectOfType<PlayersMover>();
		_nextPlayer = mover.NextPlayer;
		_text.text = $"Сейчас ходит {_nextPlayer.Name}!";
	}
}
