using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadState : State
{
    public override void Enter()
    {
        StartCoroutine(LoadSequence());
    }

    IEnumerator LoadSequence()
    {
        yield return StartCoroutine(Board.instance.InitSequence(this));
        yield return null;
        yield return StartCoroutine(LoadAnimations());
        yield return null;
        MapLoader.instance.CreateCharacters();
        yield return null;
        InitialOrdering();
        yield return null;
        UnitAlliances();
        yield return null;
        List<Vector3Int> blockers = Blockers.instance.GetBlockers();
        yield return null;
        SetBlockers(blockers);
        yield return null;
        StateMachineController.instance.ChangeTo<TurnBeginState>();
    }

    void InitialOrdering()
    {
        for (int i = 0; i < machine.units.Count; i++)
        {
            machine.units[i].chargeTime = 100 - machine.units[i].GetStat(StatEnum.SPEED);
            machine.units[i].active = true;
        }
    }

    void UnitAlliances()
    {
        for (int i = 0; i < machine.units.Count; i++)
        {
            SetUnitAlliance(machine.units[i]);
        }
    }

    void SetUnitAlliance(UnitCharacter uc)
    {
        for (int i = 0; i < MapLoader.instance.alliances.Count; i++)
        {
            if (MapLoader.instance.alliances[i].factions.Contains(uc.faction))
            {
                MapLoader.instance.alliances[i].units.Add(uc);
                uc.alliance = i;
                return;
            }
        }
    }

    void SetBlockers(List<Vector3Int> blockers)
    {
        foreach (Vector3Int pos in blockers)
        {
            TileLogic t = Board.GetTile(pos);
            t.content = Blockers.instance.gameObject;
        }
    }

    IEnumerator LoadAnimations()
    {
        SpriteLoader[] loaders = SpriteLoader.holder.GetComponentsInChildren<SpriteLoader>();
        foreach (SpriteLoader loader in loaders)
        {
            yield return loader.Load();
        }
    }
}
