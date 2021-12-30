using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAOE : AreaOfEffect
{
    public override List<TileLogic> GetArea(List<TileLogic> tiles)
    {
        return tiles;
    }
}
