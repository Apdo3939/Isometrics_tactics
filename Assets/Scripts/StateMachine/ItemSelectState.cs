using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectState : State
{
    List<Consumables> consumables;
    public override void Enter()
    {
        base.Enter();
        index = 0;
        inputs.OnMove += OnMove;
        inputs.OnFire += OnFire;
        machine.skillActionPanel.MoveTo("Show");
        machine.panelCharacterLeft.Show(Turn.unitCharacter);
        currentUISelector = machine.skillActionSelector;
        ChangeUISelector(machine.skillActionButons);
        CheckItems();
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMove;
        inputs.OnFire -= OnFire;
        machine.skillActionPanel.MoveTo("Hide");
    }

    void OnMove(object sender, object args)
    {
        Vector3Int button = (Vector3Int)args;
        if (button == Vector3Int.right || button == Vector3Int.up)
        {
            index--;
            ChangeUISelector(machine.skillActionButons);
        }
        else if (button == Vector3Int.left || button == Vector3Int.down)
        {
            index++;
            ChangeUISelector(machine.skillActionButons);
        }
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;
        if (button == 1)
        {
            ActionsButton();
        }
        else if (button == 2)
        {
            machine.panelCharacterLeft.Hide();
            Turn.isItem = null;
            machine.ChangeTo<ChooseActionState>();
        }
    }

    void CheckItems()
    {
        consumables = new List<Consumables>();
        int bag = (int)ItemSlotEnum.Bag;

        for (int i = 0; i < 1; i++, bag++)
        {
            Item item = Turn.unitCharacter.equipment.GetItem((ItemSlotEnum)bag);
            if (item != null)
            {
                consumables.Add(item.GetComponent<Consumables>());
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (i < consumables.Count)
            {
                if (consumables[i].icon == null)
                {
                    machine.skillActionButons[i].sprite = consumables[i].skill.icon;
                }
                else
                {
                    machine.skillActionButons[i].sprite = consumables[i].icon;
                }

            }
            else
            {
                machine.skillActionButons[i].sprite = machine.skillActionBlocked;
            }
        }
    }

    void ActionsButton()
    {
        if (index >= consumables.Count) { return; }

        if (consumables[index].skill.CanUse())
        {
            Debug.Log("Use... " + consumables[index].name);
            Turn.skill = consumables[index].skill;
            Turn.isItem = consumables[index];
            machine.ChangeTo<SkillTargetState>();
        }
    }
}
