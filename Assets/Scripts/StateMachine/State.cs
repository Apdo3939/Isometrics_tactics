using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    protected InputController inputs { get { return InputController.instance; } }
    protected StateMachineController machine { get { return StateMachineController.instance; } }
    protected Board board { get { return Board.instance; } }
    protected int index;
    protected Image currentUISelector;

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    protected void OnMoveTileSelector(object sender, object args)
    {
        Vector3Int input = (Vector3Int)args;
        TileLogic t = Board.GetTile(Selector.instance.position + input);

        if (t != null)
        {
            MoveSelector(t);
        }

    }

    protected void MoveSelector(Vector3Int pos)
    {
        MoveSelector(Board.GetTile(pos));
    }

    protected void MoveSelector(TileLogic t)
    {
        Selector.instance.tile = t;
        Selector.instance.sr.sortingOrder = t.contentOrder;
        Selector.instance.transform.position = t.worldPos;
        machine.selectedTile = t;
    }

    protected void ChangeUISelector(List<Image> buttons)
    {
        if (index == -1) { index = buttons.Count - 1; }
        else if (index == buttons.Count) { index = 0; }

        currentUISelector.transform.localPosition =
        buttons[index].transform.localPosition;
    }

    protected IEnumerator AIMoveSelector(Vector3Int destination)
    {
        while (Selector.instance.position != destination)
        {
            if (Selector.instance.position.x < destination.x)
            {
                OnMoveTileSelector(null, Vector3Int.right);
            }
            if (Selector.instance.position.x > destination.x)
            {
                OnMoveTileSelector(null, Vector3Int.left);
            }
            if (Selector.instance.position.y < destination.y)
            {
                OnMoveTileSelector(null, Vector3Int.up);
            }
            if (Selector.instance.position.y > destination.y)
            {
                OnMoveTileSelector(null, Vector3Int.down);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
