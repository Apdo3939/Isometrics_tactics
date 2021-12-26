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
    public string direction = "South";
    public AnimationController animationController;

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
        return stats.stats[(int)stat].value;
    }

    public void SetStat(StatEnum stat, int value)
    {
        stats.stats[(int)stat].value = GetStat(stat) + value;
    }
}
