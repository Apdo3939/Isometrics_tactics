using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int damage;
    public int manaCost;
    public Sprite icon;
    public float gotHitDelay;

    public bool CanUse()
    {
        if (Turn.unitCharacter.GetStat(StatEnum.MP) >= manaCost) { return true; }
        return false;
    }

    public bool ValidateTarget(List<TileLogic> targets)
    {
        foreach (TileLogic t in targets)
        {
            if (t.content != null && t.content.GetComponent<UnitCharacter>() != null)
            {
                return true;
            }
        }
        return false;
    }

    public List<TileLogic> GetTargets()
    {
        return GetComponentInChildren<SkillRange>().GetTilesInRange();
    }

    public void Effect()
    {
        FilterContent(Turn.targets);

        for (int i = 0; i < Turn.targets.Count; i++)
        {
            UnitCharacter uc = Turn.targets[i].content.GetComponent<UnitCharacter>();
            if (uc != null)
            {
                uc.SetStat(StatEnum.HP, -damage);

                if (uc.GetStat(StatEnum.HP) <= 0)
                {
                    uc.animationController.Death(gotHitDelay);
                }
                else
                {
                    uc.animationController.Idle();
                    uc.animationController.GotHit(gotHitDelay);
                }
            }
        }
    }

    void FilterContent(List<TileLogic> targets)
    {
        targets.RemoveAll((x) => x.content == null);
    }
}
