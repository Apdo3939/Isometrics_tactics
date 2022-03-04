using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSerialized
{
    public string charactersName;
    public string job;
    public Vector3Int position;
    public PlayerType playerType;
    public int faction;
    public int level;
    public List<Item> items;
}
