using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatText : MonoBehaviour
{
    public static CombatText instance;
    public TextMeshProUGUI prefab;
    public Vector3 offSet;
    public Vector3 random;
    public float timeToLive;

    void Awake()
    {
        instance = this;
    }

    [ContextMenu("Pop")]
    public void PopText(UnitCharacter unit, int value)
    {
        //unit = Turn.unitCharacter;
        StartCoroutine(PopControl(unit, value));
    }

    IEnumerator PopControl(UnitCharacter unit, int value)
    {
        yield return null;
        Vector3 randomPos = new Vector3(Random.Range(-random.x, random.x), Random.Range(-random.y, random.y), 0);
        TextMeshProUGUI instantiated = Instantiate(prefab, unit.transform.position + offSet + randomPos, Quaternion.identity, unit.transform.Find("Canvas"));
        instantiated.transform.SetAsLastSibling();

        if (value <= 0)
        {
            instantiated.color = Color.red;
        }
        else
        {
            instantiated.color = Color.green;
        }

        instantiated.text = "" + Mathf.Abs(value);

        LeanTween.alphaCanvas(instantiated.GetComponent<CanvasGroup>(), 1, 0.5f);
        yield return new WaitForSeconds(timeToLive);
        LeanTween.alphaCanvas(instantiated.GetComponent<CanvasGroup>(), 0, 1).setOnComplete(() =>
        {
            Destroy(instantiated.gameObject);
        });
    }
}
