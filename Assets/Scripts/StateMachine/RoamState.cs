using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
    public override void Enter()
    {
        base.Enter();
        inputs.OnMove += OnMoveTileSelector;
        inputs.OnFire += OnFire;
        CheckNullPosition();
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMoveTileSelector;
        inputs.OnFire -= OnFire;
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;
        if (button == 1)
        {
            Debug.Log("button 1 --- RoamState");
        }
        else if (button == 2)
        {
            Debug.Log("button 2 --- RoamState");
            StateMachineController.instance.ChangeTo<ChooseActionState>();
        }
    }

    void CheckNullPosition()
    {
        if (Selector.instance.tile == null)
        {
            TileLogic t = Board.GetTile(new Vector3Int(0, 0, 0));
            Selector.instance.tile = t;
            Selector.instance.sr.sortingOrder = t.contentOrder;
            Selector.instance.transform.position = t.worldPos;
        }

    }
}
