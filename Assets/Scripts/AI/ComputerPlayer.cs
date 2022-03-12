using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
    public static ComputerPlayer instance;
    UnitCharacter currentUnit { get { return Turn.unitCharacter; } }
    UnitCharacter nearestFoe;
    public AIPlan currentPlan;

    void Awake()
    {
        instance = this;
    }

    public AIPlan Evaluate()
    {

        AISkillBehavior aISkillBehavior = Turn.unitCharacter.GetComponent<AISkillBehavior>();
        if (aISkillBehavior == null)
        {
            aISkillBehavior = Turn.unitCharacter.gameObject.AddComponent<AISkillBehavior>();
        }

        AIPlan plan = new AIPlan();
        if (tryToEvaluate(plan, aISkillBehavior))
        {
            Debug.Log("OK!!!");
        }
        if (plan.skill == null)
        {
            MoveTowardOpponent(plan);
        }

        currentPlan = plan;
        return plan;
    }

    bool tryToEvaluate(AIPlan plan, AISkillBehavior aISkillBehavior)
    {
        aISkillBehavior.Pick(plan);
        if (plan.skill == null)
        {
            return false;
        }

        Debug.Log("Skill escolhida: " + plan.skill);

        if (isDirectionIndenpendent(plan))
        {
            PlanDirectionIndependent(plan);
        }
        else
        {
            PlanDirectionDependent(plan);
        }

        return true;
    }

    void FindNearestFoe()
    {
        nearestFoe = null;
        Board.instance.Search(Turn.unitCharacter.tile, delegate (TileLogic arg1, TileLogic arg2)
        {
            if (nearestFoe == null && arg2.content != null)
            {

                UnitCharacter unit = arg2.content.GetComponent<UnitCharacter>();
                if (unit != null && currentUnit.alliance != unit.alliance)
                {
                    Stats stats = unit.stats;
                    if (stats[StatEnum.HP].currentValue > 0)
                    {
                        nearestFoe = unit;
                        return true;
                    }
                }
            }
            arg2.distance = arg1.distance + 1;
            return nearestFoe == null;
        });
    }

    List<TileLogic> GetMoveOptions()
    {
        return Board.instance.Search(Turn.unitCharacter.tile,
        Turn.unitCharacter.GetComponent<Movement>().ValidateMovement);
    }

    List<TileLogic> GetMoveOptions(bool includeCurrentPos)
    {
        List<TileLogic> rtv = GetMoveOptions();
        rtv.Add(Turn.unitCharacter.tile);
        return rtv;
    }

    void MoveTowardOpponent(AIPlan plan)
    {

        List<TileLogic> moveOptions = GetMoveOptions();
        FindNearestFoe();

        if (nearestFoe != null)
        {

            TileLogic toCheck = nearestFoe.tile;

            while (toCheck != null)
            {
                if (moveOptions.Contains(toCheck))
                {
                    plan.movePos = toCheck.pos;
                    return;
                }
                toCheck = toCheck.prev;
            }
        }

        plan.movePos = Turn.unitCharacter.tile.pos;
    }

    bool isDirectionIndenpendent(AIPlan plan)
    {
        SkillRange range = plan.skill.GetComponentInChildren<SkillRange>();
        return !range.IsDirectionOriented();
    }

    void PlanDirectionDependent(AIPlan plan)
    {
        TileLogic startTile = Turn.unitCharacter.tile;
        string startDirection = Turn.unitCharacter.direction;
        TileLogic selectorStartTile = Selector.instance.tile;
        List<AttackOption> list = new List<AttackOption>();
        List<TileLogic> moveOptions = GetMoveOptions(true);
        string[] directions = new string[] { "North", "South", "East", "West" };

        for (int i = 0; i < moveOptions.Count; i++)
        {
            TileLogic moveTile = moveOptions[i];
            Turn.unitCharacter.tile.content = null;
            Turn.unitCharacter.tile = moveTile;
            moveTile.content = Turn.unitCharacter.gameObject;

            for (int j = 0; j < 4; j++)
            {
                Turn.unitCharacter.direction = directions[j];
                AttackOption ao = new AttackOption();
                ao.target = moveTile;
                ao.direction = Turn.unitCharacter.direction;
                RateFireLocation(plan, ao);
                ao.moveTargets.Add(moveTile);
                list.Add(ao);
            }
        }

        Turn.unitCharacter.tile.content = null;
        Turn.unitCharacter.tile = startTile;
        startTile.content = Turn.unitCharacter.gameObject;
        Turn.unitCharacter.direction = startDirection;
        Selector.instance.tile = selectorStartTile;
        PickBestOption(plan, list);
        Debug.LogFormat("Move go to: {0} and use skill: {1}", plan.movePos, plan.skill);
        PrintTest(list);
    }

    void PlanDirectionIndependent(AIPlan plan)
    {
        TileLogic startTile = Turn.unitCharacter.tile;
        TileLogic selectorStartTile = Selector.instance.tile;
        Dictionary<TileLogic, AttackOption> map = new Dictionary<TileLogic, AttackOption>();
        SkillRange sr = plan.skill.GetComponentInChildren<SkillRange>();
        List<TileLogic> moveOptions = GetMoveOptions(true);

        for (int i = 0; i < moveOptions.Count; i++)
        {
            TileLogic moveTile = moveOptions[i];
            Turn.unitCharacter.tile.content = null;
            Turn.unitCharacter.tile = moveTile;
            moveTile.content = Turn.unitCharacter.gameObject;
            List<TileLogic> fireOptions = sr.GetTilesInRange();

            for (int j = 0; j < fireOptions.Count; j++)
            {
                TileLogic fireTile = fireOptions[j];
                AttackOption ao = null;

                if (map.ContainsKey(fireTile))
                {
                    ao = map[fireTile];
                }
                else
                {
                    ao = new AttackOption();
                    map[fireTile] = ao;
                    ao.target = fireTile;
                    ao.direction = Turn.unitCharacter.direction;
                    RateFireLocation(plan, ao);
                }
                ao.moveTargets.Add(moveTile);
            }
        }

        Turn.unitCharacter.tile.content = null;
        Turn.unitCharacter.tile = startTile;
        startTile.content = Turn.unitCharacter.gameObject;
        Selector.instance.tile = selectorStartTile;
        List<AttackOption> list = new List<AttackOption>(map.Values);

        PickBestOption(plan, list);
        Debug.LogFormat("Move go to: {0} and use skill: {1} in the target {2}", plan.movePos, plan.skill, plan.skillTargetPos);
        PrintTest(list);
    }

    void PrintTest(List<AttackOption> list)
    {
        foreach (AttackOption ao in list)
        {
            foreach (AttackOption.Hit hit in ao.hits)
            {
                Debug.LogFormat("Hit: {0}, IsMatch: {1}", hit.tile.pos, hit.isMatch);
            }
        }
    }
    bool IsSkillTargetMatch(AIPlan plan, TileLogic tile, UnitCharacter unit)
    {
        if (unit == null)
        {
            return false;
        }

        switch (plan.targetType)
        {
            case SkillAffectsType.Default:
                return true;
            case SkillAffectsType.EnemyOnly:
                if (unit.alliance != Turn.unitCharacter.alliance)
                {
                    return true;
                }
                break;
            case SkillAffectsType.AllyOnly:
                if (unit.alliance == Turn.unitCharacter.alliance)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    void RateFireLocation(AIPlan plan, AttackOption option)
    {

        List<TileLogic> tiles = plan.skill.GetTargets();
        AreaOfEffect area = plan.skill.GetComponentInChildren<AreaOfEffect>();
        Selector.instance.tile = option.target;
        tiles = area.GetArea(tiles);
        option.areaTargets = tiles;
        option.isCasterMatch = IsSkillTargetMatch(plan, Turn.unitCharacter.tile, Turn.unitCharacter);

        for (int i = 0; i < tiles.Count; i++)
        {
            TileLogic tile = tiles[i];
            if (Turn.unitCharacter.tile == tile || tile.content == null)
            {
                continue;
            }

            UnitCharacter unit = tile.content.GetComponent<UnitCharacter>();
            if (unit == null || unit.stats[StatEnum.HP].currentValue <= 0)
            {
                continue;
            }

            bool isMatch = IsSkillTargetMatch(plan, tile, unit);
            option.AddHit(tile, isMatch);
        }
    }

    void PickBestOption(AIPlan plan, List<AttackOption> list)
    {
        int bestScore = 1;
        List<AttackOption> bestOptions = new List<AttackOption>();
        for (int i = 0; i < list.Count; i++)
        {
            AttackOption option = list[i];
            int score = option.GetScore(Turn.unitCharacter, plan.skill);
            if (score > bestScore)
            {
                bestScore = score;
                bestOptions.Clear();
                bestOptions.Add(option);
            }
            else if (score == bestScore)
            {
                bestOptions.Add(option);
            }
        }

        if (bestOptions.Count == 0)
        {
            plan.skill = null;
            return;
        }

        AttackOption choice = bestOptions[Random.Range(0, bestOptions.Count)];
        plan.skillTargetPos = choice.target.pos;
        plan.direction = choice.direction;
        plan.movePos = choice.bestMoveTile.pos;

    }
}
