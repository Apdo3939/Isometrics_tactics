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
            if (currentTile.floor != to.floor)
            {
                yield return StartCoroutine(Jump(to));
            }
            else { yield return StartCoroutine(Walk(to)); }
        }
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

    IEnumerator Jump(TileLogic to)
    {
        int id1 = LeanTween.move(transform.gameObject, to.worldPos, moveSpeed).id;
        LeanTween.moveLocalY(jumper.gameObject, jumpHeight, moveSpeed * moveSpeed)
        .setLoopPingPong(1).setEase(LeanTweenType.easeInOutQuad);

        float timerOrderUpdate = moveSpeed;
        if (currentTile.floor.tilemap.tileAnchor.y > to.floor.tilemap.tileAnchor.y)
        {
            timerOrderUpdate *= 0.85f;
        }
        else
        {
            timerOrderUpdate *= 0.2f;
        }

        yield return new WaitForSeconds(timerOrderUpdate);
        currentTile = to;
        sr.sortingOrder = to.contentOrder;

        while (LeanTween.descr(id1) != null) { yield return null; }
        //to.content = this.gameObject;
    }

    public virtual bool ValidateMovement(TileLogic from, TileLogic to)
    {
        if (Mathf.Abs(from.floor.height - to.floor.height) > 1 || to.content != null)
        {
            return true;
        }
        return false;
    }
}
