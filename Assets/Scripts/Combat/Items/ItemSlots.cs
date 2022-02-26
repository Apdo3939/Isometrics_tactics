using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemSlotEnum
{
    Head,
    UpperBody,
    LowerBody,
    MainHand,
    OffHand,
    Bag,
    None,
}


[System.Serializable]
public class ItemSlots
{
    public ItemSlotEnum slotEnum;
    public Item item;
}


