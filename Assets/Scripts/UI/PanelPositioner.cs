using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPositioner : MonoBehaviour
{
    public List<PanelPosition> positions;
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void MoveTo(string positionName)
    {
        StopAllCoroutines();
        LeanTween.cancel(this.gameObject);
        PanelPosition pos = positions.Find(x => x.name == positionName);
        StartCoroutine(Move(pos));
    }

    IEnumerator Move(PanelPosition pos)
    {
        rect.anchorMax = pos.anchorMax;
        rect.anchorMin = pos.anchorMin;
        int id = LeanTween.move(GetComponent<RectTransform>(), pos.position, 0.5f).id;
        while (LeanTween.descr(id) != null)
        {
            yield return null;
        }
    }
}
