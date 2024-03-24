using System;
using Unity.VisualScripting;
using UnityEngine;

public class BoxInteraction : AbstractInteraction
{
    public override void execute(GameObject author)
    {
        GameObject spawnedObject = Instantiate(slots.Retrieve(), author.transform.position, Quaternion.identity);
        spawnedObject.transform.parent = author.transform;
    }
}