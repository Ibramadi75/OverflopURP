using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    Left,
    Right,
    Front,
    Back,
    Stop
}

public class VehicleDirectionTrigger : MonoBehaviour
{
    [SerializeField] Direction direction = Direction.Front;

    public Direction GetDirection() => direction;
}
