using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTurnBegin();

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
    public string direction = "South";
    public AnimationController animationController;
    public OnTurnBegin onTurnBegin;

    void Awake()
    {
        stats = GetComponentInChildren<Stats>();
        SS = transform.Find("Jumper/Sprite").GetComponent<SpriteSwapper>();
        animationController = GetComponent<AnimationController>();
    }

    void Start()
    {
        SS.thisUnitSprite = SpriteLoader.holder.Find(spriteModel).GetComponent<SpriteLoader>();
        animationController.Idle();
    }

    public int GetStat(StatEnum stat)
    {
        return stats.stats[(int)stat].currentValue;
    }

    public void SetStat(StatEnum stat, int value)
    {
        //stats.stats[(int)stat].currentValue += value;
        if (stat == StatEnum.HP)
        {
            stats[stat].currentValue = ClampStat(StatEnum.MaxHP, stats[stat].currentValue + value);
        }
        else if (stat == StatEnum.MP)
        {
            stats[stat].currentValue = ClampStat(StatEnum.MaxMP, stats[stat].currentValue + value);
        }
    }

    int ClampStat(StatEnum type, int value)
    {
        return Mathf.Clamp(value, 0, stats[type].currentValue);
    }

    public void UpdateStat(StatEnum stat)
    {
        Stat toUpdate = stats.stats[(int)stat];
        toUpdate.currentValue = stats[stat].baseValue;
        if (toUpdate.modifiers != null)
        {
            toUpdate.modifiers(toUpdate);
        }
    }

    public void UpdateStat()
    {
        foreach (Stat s in stats.stats)
        {
            UpdateStat(s.type);
        }
    }
}
