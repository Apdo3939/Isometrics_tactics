using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificAOE : AreaOfEffect
{
    public int range;
    public int verticalrange;
    TileLogic tile;
    public override List<TileLogic> GetArea(List<TileLogic> tiles)
    {
        tile = Selector.instance.tile;
        return Board.instance.Search(tile, SearchType);
    }

    bool SearchType(TileLogic from, TileLogic to)
    {
        to.distance = from.distance + 1;
        return (from.distance + 1) <= range && Mathf.Abs(to.floor.height - tile.floor.height) <= verticalrange;
    }
}
