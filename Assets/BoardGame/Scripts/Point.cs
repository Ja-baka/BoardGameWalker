using UnityEngine;

public class Point : MonoBehaviour
{
    public EffectType EffectType => _effectType;
    public int EffectValue => _effectValue;
    public string Message => _message;

	[SerializeField] private EffectType _effectType;
	[SerializeField] private int _effectValue;
	[SerializeField] private string _message;
}
