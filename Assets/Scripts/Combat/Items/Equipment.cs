using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public List<ItemSlots> itemSlots;

    void Awake()
    {
        SetItemSlots();
    }

    void SetItemSlots()
    {
        itemSlots = new List<ItemSlots>();
        for (int i = 0; i <= (int)ItemSlotEnum.None; i++)
        {
            ItemSlots slot = new ItemSlots();
            slot.slotEnum = (ItemSlotEnum)i;
            itemSlots.Add(slot);
        }
    }

    public void Equip(Item item)
    {
        ItemSlots primary = itemSlots[(int)item.primarySlot];
        ItemSlots secondary = null;

        if (item.secondarySlot != ItemSlotEnum.None)
        {
            secondary = itemSlots[(int)item.secondarySlot];
        }

        if (item.useBoth)
        {
            if (primary.item == null && secondary.item == null)
            {
                primary.item = item;
                secondary.item = item;
                ActivateEquippable(item);
            }
        }
        else
        {
            if (primary.item == null)
            {
                primary.item = item;
                ActivateEquippable(item);
            }
            else if (item.secondarySlot != ItemSlotEnum.None && secondary.item == null)
            {
                secondary.item = item;
                ActivateEquippable(item);
            }
        }
    }

    public void UnEquip(Item item)
    {
        foreach (ItemSlots slot in itemSlots)
        {
            if (slot.item == item)
            {
                slot.item = null;
                Destroy(item.gameObject);
            }
        }
    }

    public Item GetItem(ItemSlotEnum slot)
    {
        return itemSlots[(int)slot].item;
    }

    void ActivateEquippable(Item item)
    {
        Equippable equippable = item.GetComponent<Equippable>();
        if (equippable != null)
        {
            UnitCharacter unit = item.GetComponentInParent<UnitCharacter>();
            equippable.Use(unit);
        }
    }
}
