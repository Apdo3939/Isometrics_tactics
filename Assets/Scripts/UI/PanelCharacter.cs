using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCharacter : MonoBehaviour
{
    public Image portrait;
    public Text unitName;
    public Text hp;
    public Text mp;
    public Text lvl;
    public Text acc;
    public Text atk;
    public Text def;
    public Text ct;

    [HideInInspector]
    public PanelPositioner positioner;

    void Awake()
    {
        positioner = GetComponent<PanelPositioner>();
    }

    void SetValues(UnitCharacter unit)
    {
        unitName.text = unit.name;

        lvl.text = "Lv. " + unit.stats[StatEnum.LVL].currentValue;

        hp.text = string.Format("HP: {0}/{1}", unit.stats[StatEnum.HP].currentValue, unit.stats[StatEnum.MaxHP].currentValue);

        mp.text = string.Format("MP: {0}/{1}", unit.stats[StatEnum.MP].currentValue, unit.stats[StatEnum.MaxMP].currentValue);

        acc.text = "AC: " + unit.stats[StatEnum.ACC].currentValue;

        atk.text = "AT: " + unit.stats[StatEnum.ATK].currentValue;

        def.text = "DF: " + unit.stats[StatEnum.DEF].currentValue;

        ct.text = "CT: " + unit.chargeTime;

        portrait.sprite = unit.job.portrait;
    }

    public void Show(UnitCharacter unit)
    {
        SetValues(unit);
        positioner.MoveTo("Show");
    }

    public void Hide()
    {
        positioner.MoveTo("Hide");
    }
}
