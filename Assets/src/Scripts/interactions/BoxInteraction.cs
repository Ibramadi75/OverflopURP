using System;
using Unity.VisualScripting;
using UnityEngine;

public class BoxInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        if (author.GetComponent<Slots>().IsEmpty())
        {
            GameObject objectToSpawn = transform.GetComponent<Slots>().Retrieve();
            Give(author, objectToSpawn, author.transform.position);
        }
    }

    public override void SecondaryInteraction(GameObject author)
    {
        MainInteraction(author);
    }
}