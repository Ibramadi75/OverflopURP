using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    protected Slots slots;

    void Start()
    {
        slots = GetComponent<Slots>();
    }

    protected void Give(GameObject receiver, GameObject objectToSpawn, Vector3 position)
    {
        GameObject spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);
        spawnedObject.transform.parent = receiver.transform;
        receiver.GetComponent<Slots>().Store(spawnedObject);
    }

    protected void Place(GameObject author, GameObject receiver)
    {
        GameObject objectToSpawn = author.GetComponent<Slots>().Retrieve();
        Give(receiver, objectToSpawn, GetTopPosition(objectToSpawn, receiver));
        Destroy(author.transform.GetComponentInChildren<Ingredient>().gameObject);
    }

    protected Vector3 GetTopPosition(GameObject objectToSpawn, GameObject receiver)
    {
        float objectHeight = objectToSpawn.GetComponent<Renderer>().bounds.size.y;
        float receiverHeight = receiver.GetComponent<Renderer>().bounds.size.y;
        return receiver.transform.position + Vector3.up * (receiverHeight / 2 + objectHeight / 2);
    }

    public abstract void MainInteraction(GameObject author);
    public abstract void SecondaryInteraction(GameObject author);
}