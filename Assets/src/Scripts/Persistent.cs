using UnityEngine;

[CreateAssetMenu(fileName = "Persistent", menuName = "Persistent", order = 0)]
public class Persistent : ScriptableObject
{
    public int day = 1;
    public int endDay = 3;
}
