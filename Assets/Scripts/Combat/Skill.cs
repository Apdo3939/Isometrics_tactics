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
            if (t.content != null)
            {
                UnitCharacter unitCharacter = t.content.GetComponent<UnitCharacter>();
                if (unitCharacter != null && GetComponentInChildren<SkillAffects>().IsTarget(unitCharacter))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public List<TileLogic> GetTargets()
    {
        return GetComponentInChildren<SkillRange>().GetTilesInRange();
    }

    public void Perform()
    {
        FilterContent(Turn.targets);

        for (int i = 0; i < Turn.targets.Count; i++)
        {
            UnitCharacter uc = Turn.targets[i].content.GetComponent<UnitCharacter>();
            if (uc != null && RollToHit(uc))
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

    public List<TileLogic> GetArea()
    {
        List<TileLogic> targets = GetComponentInChildren<AreaOfEffect>().GetArea(Turn.targets);
        return targets;
    }

    bool RollToHit(UnitCharacter uc)
    {
        bool hit = GetComponentInChildren<HitRate>().TryToHit(uc);
        if (hit)
        {
            Debug.Log("Hit!!!");
            return true;
        }
        Debug.Log("Miss!!!");
        return false;
    }

    public int GetHitPrediction(UnitCharacter target)
    {
        return GetComponentInChildren<HitRate>().Predict(target);
    }

    public int GetDamagePrediction(UnitCharacter target)
    {
        return GetComponentInChildren<SkillEffect>().Predict(target);
    }
}
