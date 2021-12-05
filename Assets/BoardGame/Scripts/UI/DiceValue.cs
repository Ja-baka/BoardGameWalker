using TMPro;
using UnityEngine;

public class DiceValue : MonoBehaviour
{
	private Dice _dice;
	private TextMeshPro _text;

	private void Awake()
	{
		_text = GetComponentInChildren<TextMeshPro>();
		_dice = FindObjectOfType<Dice>();
	}
	
	private void OnEnable()
	{
		_dice.ThrownEvent += Thrown;
		_dice.RolledEvent += Rolled;
	}


	private void OnDisable()
	{
		if (_dice != null)
		{
			_dice.ThrownEvent -= Thrown;
			_dice.RolledEvent -= Rolled;
		}
	}
	private void Thrown(object sender, DiceEventArgs e)
	{
		_text.enabled = false;
	}
	private void Rolled(object sender, DiceEventArgs e)
	{
		_text.text = e.Value.ToString();
		_text.enabled = true;
	}
}
