using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAOE : AreaOfEffect
{
    public override List<TileLogic> GetArea(List<TileLogic> tiles)
    {
        List<TileLogic> rtv = new List<TileLogic>();
        rtv.Add(Selector.instance.tile);
        return rtv;
    }
}
