using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float moveSpeed = 0.5f;
    const float jumpHeight = 0.5f;
    SpriteRenderer sr;
    Transform jumper;
    TileLogic currentTile;

    void Start()
    {
        jumper = transform.Find("Jumper");
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public IEnumerator Move(List<TileLogic> path)
    {
        currentTile = Turn.unitCharacter.tile;
        currentTile.content = null;

        for (int i = 0; i < path.Count; i++)
        {
            TileLogic to = path[i];

            Turn.unitCharacter.direction = currentTile.GetDirection(to);

            if (currentTile.floor != to.floor)
            {
                float duration = Turn.unitCharacter.animationController.Jump();
                yield return StartCoroutine(Jump(to, duration));
            }
            else
            {
                Turn.unitCharacter.animationController.Walk();
                yield return StartCoroutine(Walk(to));
            }
        }

        Turn.unitCharacter.animationController.Idle();
    }

    IEnumerator Walk(TileLogic to)
    {
        int id = LeanTween.move(transform.gameObject, to.worldPos, moveSpeed).id;
        currentTile = to;

        yield return new WaitForSeconds(moveSpeed * moveSpeed);
        sr.sortingOrder = to.contentOrder;

        while (LeanTween.descr(id) != null) { yield return null; }
        //to.content = this.gameObject;
    }

    IEnumerator Jump(TileLogic to, float duration)
    {
        yield return new WaitForSeconds(0.15f);
        float timerOrderUpdate = duration;
        if (currentTile.floor.tilemap.tileAnchor.y > to.floor.tilemap.tileAnchor.y)
        {
            timerOrderUpdate *= 0.85f;
        }
        else
        {
            timerOrderUpdate *= 0.2f;
        }

        int id1 = LeanTween.move(transform.gameObject, to.worldPos, duration).id;
        LeanTween.moveLocalY(jumper.gameObject, jumpHeight, duration * moveSpeed)
        .setLoopPingPong(1).setEase(LeanTweenType.easeInOutQuad);

        yield return new WaitForSeconds(timerOrderUpdate);
        currentTile = to;
        sr.sortingOrder = to.contentOrder;

        while (LeanTween.descr(id1) != null) { yield return null; }
        //to.content = this.gameObject;
    }

    public virtual bool ValidateMovement(TileLogic from, TileLogic to)
    {
        to.distance = from.distance + 1;

        if (to.content != null ||
            to.distance > Turn.unitCharacter.GetStat(StatEnum.MOV) ||
            Mathf.Abs(from.floor.height - to.floor.height) > 1)
        {
            return false;
        }
        return true;
    }
}
