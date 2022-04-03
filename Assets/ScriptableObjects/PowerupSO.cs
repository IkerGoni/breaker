using UnityEngine;

public enum PowerType
{
    ExtraBall,
    Bigger,
    MaxPower,
    SlowTime,
    Stick
}

[CreateAssetMenu(fileName = "PowerUp", menuName = "ScriptableObjects/PowerupDataObject", order = 1)]
public class PowerupSO : ScriptableObject
{
    public PowerType Type;
    public string DisplayName;
    public float EffectLengh;
    public float modAmount;
    public float moveSpeed;
}