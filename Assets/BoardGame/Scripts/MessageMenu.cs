using System.Collections;
using UnityEngine;
using TMPro;

public class MessageMenu : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void ShowMessage(string message)
	{
		_text = GetComponentInChildren<TextMeshProUGUI>();
		_text.text = message;
	}
}
