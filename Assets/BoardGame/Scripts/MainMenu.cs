using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	private Scrollbar _scrollbar;
	private TMP_InputField[] _inputFields;

	private void Start()
	{
		_scrollbar = GetComponentInChildren<Scrollbar>();
		_inputFields = GetComponentsInChildren<TMP_InputField>();
	}

	public void SetInputFieldInteractable()
	{
		_inputFields[0].interactable = _scrollbar.value >= 0f;
		_inputFields[1].interactable = _scrollbar.value >= 0.25f;
		_inputFields[2].interactable = _scrollbar.value >= 0.5f;
		_inputFields[3].interactable = _scrollbar.value >= 0.75f;
	}
}
