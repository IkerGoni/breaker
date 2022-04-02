using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/BrickDataObject", order = 1)]
public class BrickData : ScriptableObject
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