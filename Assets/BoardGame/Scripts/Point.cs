using UnityEngine;

public class Point : MonoBehaviour
{
    public EffectType EffectType => _effectType;
    public int EffectValue => _effectValue;
    public string Message => _message;
	public Sprite Background => _background;

	[SerializeField] private EffectType _effectType;
	[SerializeField] private int _effectValue;
	[TextArea] [SerializeField] private string _message;
	[SerializeField] private Sprite _background;
}
