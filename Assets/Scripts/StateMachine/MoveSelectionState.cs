using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectionState : State
{
    List<TileLogic> tiles; //temp for test
    public override void Enter()
    {
        base.Enter();

        MoveSelector(Turn.unitCharacter.tile);

        tiles = Board.instance.Search(Turn.unitCharacter.tile, Turn.unitCharacter.GetComponent<Movement>().ValidateMovement);//change here if create a mess!!!
        tiles.Remove(Turn.unitCharacter.tile);
        Board.instance.SelectTiles(tiles, Turn.unitCharacter.alliance);

        if (Turn.unitCharacter.playerType == PlayerType.Human)
        {
            inputs.OnMove += OnMoveTileSelector;
            inputs.OnFire += OnFire;
        }
        else
        {
            StartCoroutine(ComputerSelectMovetarget());
        }
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMoveTileSelector;
        inputs.OnFire -= OnFire;
        //test
        Board.instance.DeSelectTiles(tiles);
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;
        if (button == 1)
        {
            if (tiles.Contains(machine.selectedTile))
                machine.ChangeTo<MoveSequenceState>();
        }
        else if (button == 2)
        {
            machine.ChangeTo<ChooseActionState>();
        }
    }

    IEnumerator ComputerSelectMovetarget()
    {
        AIPlan plan = ComputerPlayer.instance.currentPlan;
        while (Selector.instance.position != plan.movePos)
        {
            if (Selector.instance.position.x < plan.movePos.x)
            {
                OnMoveTileSelector(null, Vector3Int.right);
            }
            if (Selector.instance.position.x > plan.movePos.x)
            {
                OnMoveTileSelector(null, Vector3Int.left);
            }
            if (Selector.instance.position.y < plan.movePos.y)
            {
                OnMoveTileSelector(null, Vector3Int.up);
            }
            if (Selector.instance.position.y > plan.movePos.y)
            {
                OnMoveTileSelector(null, Vector3Int.down);
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        machine.ChangeTo<MoveSequenceState>();
    }
}
