using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    float collectedItemCount = 0;

    void Start ()
    {
        Item.OnPickUp += OnItemPickUp;
    }

    void OnItemPickUp (Item item) {
        collectedItemCount += 1;
        Debug.Log("Collected items: " + collectedItemCount);
    }
}
