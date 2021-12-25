using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacter : MonoBehaviour
{
    public Stats stats;
    public int faction;
    public int alliance;
    public TileLogic tile;
    public int chargeTime;
    public bool active;
    public string spriteModel;
    public SpriteSwapper SS;

    void Awake()
    {
        stats = GetComponentInChildren<Stats>();
        SS = transform.Find("Jumper/Sprite").GetComponent<SpriteSwapper>();
    }

    void Start()
    {
        SS.thisUnitSprite = SpriteLoader.holder.Find(spriteModel).GetComponent<SpriteLoader>();
        SS.PlayAnimation("IdleSouth");
    }

    public int GetStat(StatEnum stat)
    {
        return stats.stats[(int)stat].value;
    }

    public void SetStat(StatEnum stat, int value)
    {
        stats.stats[(int)stat].value = GetStat(stat) + value;
    }
}
