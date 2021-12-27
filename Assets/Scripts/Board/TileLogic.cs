using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic
{
    public Vector3Int pos;
    public Vector3 worldPos;
    public GameObject content;
    public Floor floor;
    public int contentOrder;

    #region Pathfinding
    public TileLogic prev;
    public float distance;
    #endregion

    public TileLogic() { }

    public TileLogic(Vector3Int cellPos, Vector3 worldPosition, Floor tempFloor)
    {
        pos = cellPos;
        worldPos = worldPosition;
        floor = tempFloor;
        contentOrder = tempFloor.orderContent;
    }

    public static TileLogic Create(Vector3Int cellPos, Vector3 worldPosition, Floor tempFloor)
    {
        TileLogic tileLogic = new TileLogic(cellPos, worldPosition, tempFloor);
        return tileLogic;
    }

    public string GetDirection(TileLogic t2)
    {
        if (this.pos.y < t2.pos.y) { return "North"; }
        if (this.pos.x < t2.pos.x) { return "East"; }
        if (this.pos.y > t2.pos.y) { return "South"; }
        return "West";
    }

    public string GetDirection(Vector3Int t2)
    {
        if (this.pos.y < t2.y) { return "North"; }
        if (this.pos.x < t2.x) { return "East"; }
        if (this.pos.y > t2.y) { return "South"; }
        return "West";
    }

}
