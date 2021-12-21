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
        UnitCharacter uc01 = CreateUnit(new Vector3Int(0, 2, 0), "Jogador01");
        UnitCharacter uc02 = CreateUnit(new Vector3Int(0, -5, 0), "Inimigo01");

        StateMachineController.instance.units.Add(uc01);
        StateMachineController.instance.units.Add(uc02);

        uc01.faction = 0;
        uc02.faction = 1;
    }

    public UnitCharacter CreateUnit(Vector3Int pos, string name)
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

        for (int i = 0; i < uc.stats.stats.Count; i++)
        {
            if (uc.stats.stats[i].type != StatEnum.MOV)
            {
                uc.stats.stats[i].value = Random.Range(1, 100);
            }
            else
            {
                uc.stats.stats[i].value = Random.Range(3, 7);
            }

        }

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
