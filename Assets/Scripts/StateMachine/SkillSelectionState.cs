using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionState : State
{
    List<Skill> skills;
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
        CheckSkills();
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
            machine.ChangeTo<ChooseActionState>();
        }
    }

    void CheckSkills()
    {
        Transform skillBook = Turn.unitCharacter.transform.Find("SkillBook");
        skills = new List<Skill>();
        skills.AddRange(skillBook.GetComponent<Skillbook>().skills);

        for (int i = 0; i < 5; i++)
        {
            if (i < skills.Count) { machine.skillActionButons[i].sprite = skills[i].icon; }
            else { machine.skillActionButons[i].sprite = machine.skillActionBlocked; }
        }
    }

    void ActionsButton()
    {
        if (index >= skills.Count) { return; }
        if (skills[index].CanUse())
        {
            Turn.skill = skills[index];
            machine.ChangeTo<SkillTargetState>();
        }
    }
}
