using System;
using Unity.VisualScripting;
using UnityEngine;

public class BoxInteraction : AbstractInteraction
{
    public override void execute(GameObject author)
    {
        if (author.GetComponent<Slots>().IsEmpty())
            Give(gameObject, author);
    }
}