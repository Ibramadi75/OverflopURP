using System;
using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    protected Slots slots;

    void Start()
    {
        slots = GetComponent<Slots>();
    }

    protected void Give(GameObject author, GameObject receiver)
    {
        GameObject spawnedObject = Instantiate(author.GetComponent<Slots>().Retrieve(), receiver.transform.position, Quaternion.identity);
        spawnedObject.transform.parent = receiver.transform;
        receiver.GetComponent<Slots>().Store(spawnedObject);
    }

    public abstract void execute(GameObject author);
}