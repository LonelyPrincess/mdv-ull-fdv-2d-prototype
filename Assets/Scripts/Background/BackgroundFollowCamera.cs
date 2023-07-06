using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollowCamera : MonoBehaviour
{
    private Vector2 initialOffset;

    // Camera that will be used to do the background scrolling
    public Camera activeCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (!activeCamera) {
            Debug.LogWarning("You must specify a camera for this script to work properly!");
            return;
        }

        initialOffset = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 camStart = activeCamera.transform.position;
        float distance = Vector2.Distance(this.transform.position, camStart);

        // Background will constantly follow camera
        if (distance != 0) {
            this.transform.position = camStart + initialOffset;
        }
    }
}
