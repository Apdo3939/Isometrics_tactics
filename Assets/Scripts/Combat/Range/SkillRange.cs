using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillRange : MonoBehaviour
{
    public int range;
    public int verticalRange;
    public bool directionOriented;
    public abstract List<TileLogic> GetTilesInRange(Board board);
}
