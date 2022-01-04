using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public static MapLoader instance;
    public UnitCharacter unitCharacter;
    GameObject holder;
    public List<Alliance> alliances;

    void Awake()
    {
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
        UnitCharacter uc01 = CreateUnit(new Vector3Int(0, 2, 0), "Jogador01", "Mini-Crusader");
        UnitCharacter uc02 = CreateUnit(new Vector3Int(0, -5, 0), "Inimigo01", "Mini-Crusader");

        StateMachineController.instance.units.Add(uc01);
        StateMachineController.instance.units.Add(uc02);

        uc01.faction = 0;
        uc02.faction = 1;
    }

    public UnitCharacter CreateUnit(Vector3Int pos, string name, string spriteModel)
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

        t.content = uc.gameObject;

        for (int i = 0; i < uc.stats.stats.Count; i++)
        {
            if (uc.stats.stats[i].type != StatEnum.MOV)
            {
                uc.stats.stats[i].baseValue = Random.Range(5, 100);
            }
            else
            {
                uc.stats.stats[i].baseValue = Random.Range(2, 6);
            }
        }

        uc.stats[StatEnum.HP].baseValue = uc.stats[StatEnum.MaxHP].baseValue;
        uc.stats[StatEnum.MP].baseValue = uc.stats[StatEnum.MaxMP].baseValue;
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

}
