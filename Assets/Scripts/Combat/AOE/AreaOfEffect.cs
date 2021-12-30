using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaOfEffect : MonoBehaviour
{
    public abstract List<TileLogic> GetArea(List<TileLogic> tiles);
}
