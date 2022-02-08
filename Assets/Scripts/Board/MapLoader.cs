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
        CreateUnit(new Vector3Int(0, 1, 0), "Jogador01", "Mini-Crusader", "Mage", 0);
        CreateUnit(new Vector3Int(0, 2, 0), "Jogador02", "Mini-Crusader", "Knight", 0);
        CreateUnit(new Vector3Int(0, 3, 0), "Jogador03", "Mini-Crusader", "Warrior", 0);

        CreateUnit(new Vector3Int(0, -4, 0), "Inimigo01", "Mini-Crusader", "Knight", 1);
        CreateUnit(new Vector3Int(0, -5, 0), "Inimigo02", "Mini-Crusader", "Mage", 1);
        CreateUnit(new Vector3Int(0, -6, 0), "Inimigo03", "Mini-Crusader", "Warrior", 1);
    }

    public UnitCharacter CreateUnit(Vector3Int pos, string name, string spriteModel, string job, int faction)
    {
        TileLogic t = Board.GetTile(pos);
        UnitCharacter uc = Instantiate(
            unitCharacter,
            t.worldPos,
            Quaternion.identity,
            holder.transform
        );
        uc.tile = t;
        uc.name = name;
        t.content = uc.gameObject;
        uc.spriteModel = spriteModel;
        uc.faction = faction;
        StateMachineController.instance.units.Add(uc);

        t.content = uc.gameObject;

        uc.stats.stats = searchJobs[job].stats;
        uc.UpdateStat();

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

}
