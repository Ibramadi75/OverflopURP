using System;
using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    protected Slots slots;

    void Start()
    {
        slots = GetComponent<Slots>();
    }

    public abstract void execute(GameObject author);
}