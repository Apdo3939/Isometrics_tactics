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
        CreateUnit(new Vector3Int(-7, 3, 0), "Jogador01", "Mage", 0);
        CreateUnit(new Vector3Int(-7, 2, 0), "Jogador02", "Knight", 0);
        CreateUnit(new Vector3Int(-7, 1, 0), "Jogador03", "Warrior", 0);

        CreateUnit(new Vector3Int(0, -4, 0), "Inimigo01", "Knight", 1);
        CreateUnit(new Vector3Int(0, -5, 0), "Inimigo02", "Mage", 1);
        CreateUnit(new Vector3Int(0, -6, 0), "Inimigo03", "Warrior", 1);
    }

    public UnitCharacter CreateUnit(Vector3Int pos, string name, string job, int faction)
    {
        TileLogic t = Board.GetTile(pos);

        UnitCharacter uc = Instantiate(
            unitCharacter,
            t.worldPos,
            Quaternion.identity,
            holder.transform
        );

        Job jobAsset = searchJobs[job];
        uc.tile = t;
        uc.name = name;
        t.content = uc.gameObject;
        uc.spriteModel = jobAsset.spriteModel;
        uc.faction = faction;
        StateMachineController.instance.units.Add(uc);

        t.content = uc.gameObject;

        SetStats(uc.stats, jobAsset);
        uc.UpdateStat();

        Skillbook skillbook = uc.GetComponentInChildren<Skillbook>();
        skillbook.skills = new List<Skill>();
        skillbook.skills.AddRange(jobAsset.skills);

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

    void SetStats(Stats stats, Job job)
    {
        stats.stats = new List<Stat>();
        for (int i = 0; i < job.stats.Count; i++)
        {
            Stat stat = new Stat();
            stat.baseValue = job.stats[i].baseValue;
            stat.currentValue = job.stats[i].currentValue;
            stat.growth = job.stats[i].growth;
            stat.type = job.stats[i].type;
            stats.stats.Add(stat);
        }

        stats.stats[(int)StatEnum.HP].baseValue = stats.stats[(int)StatEnum.MaxHP].baseValue;
        stats.stats[(int)StatEnum.MP].baseValue = stats.stats[(int)StatEnum.MaxMP].baseValue;
    }

}
