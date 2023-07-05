using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class UnlockableBarrier : MonoBehaviour
{
    bool currentlyLocked = true;
    Dictionary<int, int> currentlyActiveButtons;
    public List<ActionableButton> buttonsRequiredToUnlock;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonsRequiredToUnlock.Count == 0) {
            Debug.LogWarning("No buttons have been specified to open this barrier, so it cannot be unlocked");
            return;
        }

        currentlyActiveButtons = new Dictionary<int, int>();

        ActionableButton.OnButtonPress += OnButtonPress;
        ActionableButton.OnButtonRelease += OnButtonRelease;
    }

    void UnlockBarrier () {
        Debug.Log("Opening barrier " + this.gameObject.name);

        // Apply visual effect so players know something is happening
        CinemachineImpulseSource impulseSource = GetComponent<CinemachineImpulseSource>();
        impulseSource.GenerateImpulse();

        // Disable barrier so it's no longer visible and characters can move forward
        this.gameObject.SetActive(false);
        currentlyLocked = false;
    }

    void OnButtonPress (ActionableButton button) {
        if (buttonsRequiredToUnlock.Contains(button)) {
            UpdateCurrentActiveButtonState(button, 1);
            Debug.Log(currentlyActiveButtons.Count + " active buttons for " + this.gameObject.name);
        }

        // Unlock barrier only when all buttons are active at the same time
        if (currentlyLocked && currentlyActiveButtons.Count == buttonsRequiredToUnlock.Count) {
            UnlockBarrier();
        }
    }

    void OnButtonRelease (ActionableButton button) {
        if (buttonsRequiredToUnlock.Contains(button)) {
            UpdateCurrentActiveButtonState(button, -1);
            Debug.Log(currentlyActiveButtons.Count + " active buttons for " + this.gameObject.name);
        }
    }

    // In case multiple elements are overlapping with the button, make sure we don't delete it from dictionary until all collisions have ended
    void UpdateCurrentActiveButtonState (ActionableButton button, int increase) {
        int buttonId = button.GetInstanceID();
        if (currentlyActiveButtons.ContainsKey(buttonId)) {
            int activeCollisionCount = currentlyActiveButtons[buttonId] + increase;
            if (activeCollisionCount == 0) {
                currentlyActiveButtons.Remove(buttonId);
            } else {
                currentlyActiveButtons[buttonId] = activeCollisionCount;
            }
        } else if (increase > 0) {
            currentlyActiveButtons.Add(buttonId, increase);
        }
    }
}
