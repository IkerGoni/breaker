using UnityEngine;

[CreateAssetMenu(fileName = "BrickData", menuName = "ScriptableObjects/BrickDataObject", order = 2)]
public class BrickDataSO : ScriptableObject
{
    [SerializeField]
    // public LevelLayout LevelLayout;
    public BrickLevelData[] BrickLevelsData = new BrickLevelData[3];

}
[System.Serializable]
public struct BrickLevelData
{
    public Color Color;
    public int Level;
    public int Points;
    public float powerUpProb;
}