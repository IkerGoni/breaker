using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDataObject", order = 1)]
public class LevelSO : ScriptableObject
{
    [SerializeField]
   // public LevelLayout LevelLayout;
    public ArrayLayout[] LevelLayout = new ArrayLayout[1];

}
