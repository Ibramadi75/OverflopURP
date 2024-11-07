using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Persistent", menuName = "Persistent", order = 0)]
public class Persistent : ScriptableObject
{
    private int _day = 0;
    private int _endDay = 3;

    public int UpdateDay()
    {
        if (_day >= _endDay)
        {
            // end game logic            
        }
        return _day++;
    }
}
