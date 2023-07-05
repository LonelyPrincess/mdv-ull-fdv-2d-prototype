using UnityEngine;

public class Item : MonoBehaviour
{
    // Custom event for picking up collectibles
    public delegate void PickUpEventHandler (Item item, GameObject itemPicker);
    public static event PickUpEventHandler OnPickUp;

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (OnPickUp == null) {
            Debug.LogWarning("No listeners subscribed to pick up event!");
            return;
        }

        // Trigger event is collision object has item collector script
        if (collision.gameObject.GetComponent<ItemCollector>()) {
            OnPickUp(this, collision.gameObject);
        }
    }
}
