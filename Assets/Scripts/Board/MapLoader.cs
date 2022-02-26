using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public static MapLoader instance;
    public UnitCharacter unitCharacter;
    GameObject holder;
    public List<Alliance> alliances;
    public List<Job> jobs;
    public Dictionary<string, Job> searchJobs;
    public List<UnitSerialized> serializedUnit;

    void Awake()
    {
        BuildJobsDictionary();
        instance = this;
        holder = new GameObject("Characters holder");
    }

    void Start()
    {
        holder.transform.parent = Board.instance.transform;
        InitializeAlliances();
    }

    public void CreateCharacters()
    {
        for (int i = 0; i < serializedUnit.Count; i++)
        {
            CreateUnit(serializedUnit[i]);
        }
    }

    public UnitCharacter CreateUnit(UnitSerialized serialized)
    {
        TileLogic t = Board.GetTile(serialized.position);

        UnitCharacter uc = Instantiate(
            unitCharacter,
            t.worldPos,
            Quaternion.identity,
            holder.transform
        );

        uc.tile = t;
        uc.name = serialized.charactersName;
        t.content = uc.gameObject;
        uc.faction = serialized.faction;
        StateMachineController.instance.units.Add(uc);

        Job jobAsset = searchJobs[serialized.job];
        Job.Employ(uc, jobAsset, serialized.level);

        CreateItems(serialized.items, uc);

        uc.experience = Job.GetExpCurveValue(serialized.level);

        return uc;
    }

    void InitializeAlliances()
    {
        for (int i = 0; i < alliances.Count; i++)
        {
            alliances[i].units = new List<UnitCharacter>();
        }
    }

    void BuildJobsDictionary()
    {
        searchJobs = new Dictionary<string, Job>();
        foreach (Job j in jobs)
        {
            searchJobs.Add(j.name, j);
        }
    }

    void CreateItems(List<Item> items, UnitCharacter unit)
    {
        Transform itemHolder = unit.transform.Find("Equipment");
        for (int i = 0; i < items.Count; i++)
        {
            CreateItems(items[i], unit, itemHolder);
        }
    }

    void CreateItems(Item item, UnitCharacter unit, Transform holder)
    {
        Item instantiated = Instantiate(item, unit.transform.position, Quaternion.identity, holder);
        unit.equipment.Equip(instantiated);
        instantiated.name = instantiated.name.Replace("(Clone)", "");
    }

}
