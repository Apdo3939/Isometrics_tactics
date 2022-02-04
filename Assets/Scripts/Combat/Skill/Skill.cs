using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int manaCost;
    public Sprite icon;
    Transform _primary;
    Transform primary
    {
        get
        {
            if (_primary == null)
            {
                _primary = transform.Find("Primary");
                secondary = transform.Find("Secondary");
            }
            return _primary;
        }
    }
    Transform secondary;

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
            if (uc == null)
            {
                continue;
            }

            bool didHit = RollToHit(uc, primary);
            VFX(uc, didHit);

            if (didHit)
            {
                primary.GetComponentInChildren<SkillEffect>().Apply(uc);
                if (secondary.childCount != 0 && RollToHit(uc, secondary))
                {
                    secondary.GetComponentInChildren<SkillEffect>().Apply(uc);
                }
            }
        }
    }

    void FilterContent(List<TileLogic> targets)
    {
        targets.RemoveAll((x) => x.content == null);
    }

    void VFX(UnitCharacter target, bool didHit)
    {
        SkillVisualFX fx = GetComponentInChildren<SkillVisualFX>();
        if (fx != null)
        {
            fx.target = target;
            fx.didHit = didHit;
            fx.VFX();
        }
    }

    public List<TileLogic> GetArea()
    {
        List<TileLogic> targets = GetComponentInChildren<AreaOfEffect>().GetArea(Turn.targets);
        return targets;
    }

    bool RollToHit(UnitCharacter uc, Transform effect)
    {
        bool hit = effect.GetComponentInChildren<HitRate>().TryToHit(uc);
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
        return primary.GetComponentInChildren<HitRate>().Predict(target);
    }

    public int GetEffectPrediction(UnitCharacter target)
    {
        return primary.GetComponentInChildren<SkillEffect>().Predict(target);
    }

    public int GetHitPrediction(UnitCharacter target, Transform effect)
    {
        return effect.GetComponentInChildren<HitRate>().Predict(target);
    }

    public int GetEffectPrediction(UnitCharacter target, Transform effect)
    {
        return effect.GetComponentInChildren<SkillEffect>().Predict(target);
    }
}
