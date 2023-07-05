using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionableButton : MonoBehaviour
{
    // Custom events for actionable buttons
    public delegate void PressButtonEventHandler (ActionableButton button);
    public static event PressButtonEventHandler OnButtonPress;

    public delegate void ReleaseButtonEventHandler (ActionableButton button);
    public static event ReleaseButtonEventHandler OnButtonRelease;

    // Trigger event when player collides with the button
    private void OnTriggerEnter(Collider collider)
    {
        if (OnButtonPress == null) {
            Debug.LogWarning("No listeners subscribed to button press event!");
            return;
        }

        GameObject eventSource = collider.gameObject;
        if (eventSource.CompareTag("Player")) {
            OnButtonPress(this);
        }
    }

    // Trigger event when player stops colliding with button
    private void OnTriggerExit(Collider collider) {
        if (OnButtonRelease == null) {
            Debug.LogWarning("No listeners subscribed to button release event!");
            return;
        }

        GameObject eventSource = collider.gameObject;
        if (eventSource.CompareTag("Player")) {
            OnButtonRelease(this);
        }
    }
}
