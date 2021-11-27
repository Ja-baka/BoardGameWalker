using System.Collections;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    private TextMeshPro _text;
    private Player _activePlayer;

	private void Start()
	{
		_text = FindObjectOfType<TextMeshPro>();
		PlayersMover mover = FindObjectOfType<PlayersMover>();
		_activePlayer = mover.ActivePlayer;
	}
}
