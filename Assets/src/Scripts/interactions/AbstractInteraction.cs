using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    protected Slots slots;

    void Start()
    {
        slots = GetComponent<Slots>();
    }

    /// <summary>
    /// Give the objectToSpawn to the receiver, at the position of the receiver, as a children of the receiver.
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="objectToSpawn"></param>
    /// <param name="position"></param>
    protected void Give(GameObject receiver, GameObject objectToSpawn, Vector3 position)
    {
        // GameObject spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity); // crée un nouvel objet à partir du prefab
        // spawnedObject.transform.parent = receiver.transform; // associe le parent de l'objet à l'auteur
        // receiver.GetComponent<Slots>().Store(spawnedObject);
    }

    /// <summary>
    /// Place the object stored in the author slot on the receiver and destroy it on the author.
    /// </summary>
    /// <param name="author"></param>
    /// <param name="receiver"></param>
    protected void Place(GameObject author, GameObject receiver)
    {
        GameObject objectToSpawn = author.GetComponent<Slots>().Retrieve(); // récupère l'objet à placer depuis l'inventaire de l'auteur
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