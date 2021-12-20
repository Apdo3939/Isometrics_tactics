using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public static Selector instance;
    public Vector3Int position { get { return tile.pos; } }
    public TileLogic tile;
    public SpriteRenderer sr;

    void Awake()
    {
        instance = this;
        sr = GetComponentInChildren<SpriteRenderer>();
    }
}
