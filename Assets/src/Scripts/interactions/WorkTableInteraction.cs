using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkTableInteraction : AbstractInteraction
{
    public override void execute(GameObject author)
    {
        if (slots.IsEmpty())
        {
            Give(author, gameObject);
        }
    }
}
