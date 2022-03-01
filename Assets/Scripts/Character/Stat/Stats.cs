using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MultiplicativeMods(object args);

public class Stats : MonoBehaviour
{
    public List<Stat> stats;
    public MultiplicativeMods multiplicativeModifier;

    public Stat this[StatEnum s]
    {
        get { return stats[(int)s]; }
    }

}
