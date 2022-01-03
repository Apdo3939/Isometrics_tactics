using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public List<Stat> stats;

    public int this[StatEnum s]
    {
        get { return stats[(int)s].baseValue; }
        set { stats[(int)s].baseValue = value; }
    }

    private void Awake()
    {
        stats = new List<Stat>();
        int lenght = (int)StatEnum.MOV;

        for (int i = 0; i <= lenght; i++)
        {
            Stat temp = new Stat();
            temp.type = (StatEnum)i;
            stats.Add(temp);
        }
    }
}
