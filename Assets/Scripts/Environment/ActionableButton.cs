using UnityEngine;

public class ActionableButton : MonoBehaviour
{
    // Custom events for actionable buttons
    public delegate void PressButtonEventHandler (ActionableButton button);
    public static event PressButtonEventHandler OnButtonPress;

    public delegate void ReleaseButtonEventHandler (ActionableButton button);
    public static event ReleaseButtonEventHandler OnButtonRelease;

    // Validate if the source of the collission is the player or a box the player pushed on top
    bool isEventSourceValid (Collider2D collider) {
        GameObject eventSource = collider.gameObject;
        return eventSource.CompareTag("Player") || eventSource.CompareTag("Box");
    }

    // Trigger event when player collides with the button
    private void OnTriggerEnter2D (Collider2D collider)
    {
        if (OnButtonPress == null) {
            Debug.LogWarning("No listeners subscribed to button press event!");
            return;
        }

        if (isEventSourceValid(collider)) {
            OnButtonPress(this);
        }
    }

    // Trigger event when player stops colliding with button
    private void OnTriggerExit2D (Collider2D collider) {
        if (OnButtonRelease == null) {
            Debug.LogWarning("No listeners subscribed to button release event!");
            return;
        }

        if (isEventSourceValid(collider)) {
            OnButtonRelease(this);
        }
    }
}
