using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class UnlockableBarrier : MonoBehaviour
{
    bool currentlyLocked = true;
    float currentlyActiveButtons = 0;
    public List<ActionableButton> buttonsRequiredToUnlock;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonsRequiredToUnlock.Count == 0) {
            Debug.LogWarning("No buttons have been specified to open this barrier, so it cannot be unlocked");
            return;
        }

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
    }

    void OnButtonPress (ActionableButton button) {
        if (buttonsRequiredToUnlock.Contains(button)) {
            Debug.Log(currentlyActiveButtons + " active buttons for " + this.gameObject.name);
            currentlyActiveButtons ++;
        }

        if (currentlyLocked && currentlyActiveButtons == buttonsRequiredToUnlock.Count) {
            UnlockBarrier();
        }
    }

    void OnButtonRelease (ActionableButton button) {
        if (buttonsRequiredToUnlock.Contains(button)) {
            Debug.Log(currentlyActiveButtons + " active buttons for " + this.gameObject.name);
            currentlyActiveButtons--;
        }
    }
}
