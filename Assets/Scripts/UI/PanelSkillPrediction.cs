using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSkillPrediction : MonoBehaviour
{
    [HideInInspector]
    public Text skillName;
    [HideInInspector]
    public Text chanceToHit;
    [HideInInspector]
    public Text predictEffect;
    [HideInInspector]
    public PanelPositioner positioner;

    void Awake()
    {
        positioner = GetComponent<PanelPositioner>();
        skillName = transform.Find("SkillName").GetComponent<Text>();
        chanceToHit = transform.Find("ChanceToHit").GetComponent<Text>();
        predictEffect = transform.Find("PredictEffect").GetComponent<Text>();
    }

    public void SetPredictionText()
    {
        skillName.text = Turn.skill.name;
        int toHit = 0;
        int predict = 0;
        UnitCharacter target = null;

        foreach (TileLogic t in Turn.targets)
        {
            if (t.content != null)
            {
                target = t.content.GetComponent<UnitCharacter>();
                if (target != null)
                    break;
            }
        }

        if (target != null)
        {
            toHit = Turn.skill.GetHitPrediction(target);
            predict = Turn.skill.GetEffectPrediction(target);
        }

        chanceToHit.text = Mathf.Clamp(toHit, 0, 100) + "% chance to hit";
        predictEffect.text = predict + " hitpoints";
    }
}
