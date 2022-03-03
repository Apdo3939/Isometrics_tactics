using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnTurnBegin();

public class UnitCharacter : MonoBehaviour
{
    public Stats stats;
    public int faction;
    public int alliance;
    public TileLogic tile;
    public int chargeTime;
    public bool _active;
    public string spriteModel;
    public SpriteSwapper SS;
    public string direction = "South";
    public AnimationController animationController;
    public OnTurnBegin onTurnBegin;
    public Image lifeBar;
    public AudioSource audioSource;
    public int experience;
    public Job job;
    public Equipment equipment;
    public bool active
    {
        get { return _active; }
        set
        {
            if (_active && !value)
            {
                GiveExp();
            }
            _active = value;
        }
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        stats = GetComponentInChildren<Stats>();
        SS = transform.Find("Jumper/Sprite").GetComponent<SpriteSwapper>();
        animationController = GetComponent<AnimationController>();
        equipment = GetComponentInChildren<Equipment>();
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
        if (stat == StatEnum.HP)
        {
            stats[stat].currentValue = ClampStat(StatEnum.MaxHP, stats[stat].currentValue + value);
            PopCombatText(value);
            SetLifeBar();
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
        Stat toUpdate = stats[stat];
        toUpdate.currentValue = stats[stat].baseValue;
        if (toUpdate.statModifiers != null)
        {
            toUpdate.statModifiers(toUpdate);
        }
    }

    public void UpdateStat()
    {
        foreach (Stat s in stats.stats)
        {
            UpdateStat(s.type);
        }
    }

    void SetLifeBar()
    {
        float maxHp = (float)GetStat(StatEnum.MaxHP);
        float fillvalue = (stats[StatEnum.HP].currentValue * 100 / maxHp) / 100;
        lifeBar.fillAmount = fillvalue;

        if (fillvalue >= 0.75)
        {
            lifeBar.color = Color.green;
        }
        else if (fillvalue >= 0.25)
        {
            lifeBar.color = Color.yellow;
        }
        else
        {
            lifeBar.color = Color.red;
        }
    }

    void PopCombatText(int value)
    {
        CombatText.instance.PopText(this, value);
    }

    void GiveExp()
    {
        foreach (UnitCharacter u in StateMachineController.instance.units)
        {
            if (u.alliance != alliance)
            {
                u.experience += 1000;
                Job.CheckLevelUp(u);
            }
        }
    }
}
