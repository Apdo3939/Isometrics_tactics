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

    void Awake()
    {
        instance = this;
    }

    [ContextMenu("Pop")]
    public void PopText()
    {
        UnitCharacter unit = Turn.unitCharacter;
        Vector3 randomPos = new Vector3(Random.Range(-random.x, random.x), Random.Range(-random.y, random.y), 0);
        TextMeshProUGUI instantiated = Instantiate(prefab, unit.transform.position + offSet + randomPos, Quaternion.identity, unit.transform.Find("Canvas"));
        instantiated.transform.SetAsLastSibling();

        LeanTween.alphaCanvas(instantiated.GetComponent<CanvasGroup>(), 1, 0.5f);
    }
}
