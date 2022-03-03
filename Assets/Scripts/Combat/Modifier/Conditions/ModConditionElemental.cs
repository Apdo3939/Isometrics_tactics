using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModConditionElemental : ModifierCondition
{
    public ElementalType type;
    public override bool Validate(object arg1)
    {
        MultiplicativeForms forms = (MultiplicativeForms)arg1;

        if (forms.elementalType == type)
        {
            return true;
        }
        else if (forms.elementalType == ElementalType.Weapon)
        {
            Item item = Turn.unitCharacter.equipment.GetItem(ItemSlotEnum.MainHand);
            if (item == null) { return false; }

            ElementalModifier mod = item.GetComponent<ElementalModifier>();
            if (mod != null && mod.type == type)
            {
                Debug.Log("weapon elemental used!!!");
                return true;
            }
        }
        return false;
    }
}
