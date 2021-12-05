using TMPro;
using UnityEngine;

public class DiceValue : MonoBehaviour
{
	private Dice _dice;
	private TextMeshPro _text;
	private PlayersMover _playersMover;

	private void Awake()
	{
		_text = GetComponentInChildren<TextMeshPro>();
		_dice = FindObjectOfType<Dice>();
		_playersMover = Resources.FindObjectsOfTypeAll<PlayersMover>()[0];
	}
	
	private void OnEnable()
	{
		_playersMover.FinishMoveEvent += HideValue;
		_dice.RolledEvent += ShowValue;
	}

	private void OnDisable()
	{
		if (_dice != null
			&& _playersMover != null)
		{
			_playersMover.FinishMoveEvent -= HideValue;
			_dice.RolledEvent -= ShowValue;
		}
	}
	private void HideValue(object sender, PlayerEvent e)
	{
		_text.enabled = false;
	}
	private void ShowValue(object sender, DiceEventArgs e)
	{
		_text.text = e.Value.ToString();
		_text.enabled = true;
	}
}
