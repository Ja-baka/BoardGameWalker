using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MessageMenu : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Image _backgroundImage;

    private void Awake()
    {
		_text = GetComponentInChildren<TextMeshProUGUI>();
        Background bg = GetComponentInChildren<Background>();
        _backgroundImage = bg.GetComponent<Image>();
    }        

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void ShowMessage(string message, Sprite background)
	{
		_text.text = message;
		_backgroundImage.sprite = background;
	} 

    public void ShowMessage(string message)
	{
		_text.text = message;
	}
}
