using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    float minScale = 0.5f;
    float maxScale = 2.0f;
    float currentScale = 1.0f;
    bool isFlameGrowing = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ResizeFlame();
        this.transform.Rotate(new Vector3(0, 0, 1.0f));

        // TODO: trigger this when flame switch is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("flame should fly");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.left * 10.0f;
        }
    }

    // Modify the size of the flame on each render to simulate an animation
    void ResizeFlame () {
        if (isFlameGrowing) {
            currentScale += 0.05f;
            if (currentScale > maxScale) {
                currentScale = maxScale;
                isFlameGrowing = false;
            }
        } else {
            currentScale -= 0.05f;
            if (currentScale < minScale) {
                currentScale = minScale;
                isFlameGrowing = true;
            }
        }

        this.transform.localScale = new Vector3(currentScale, currentScale);
    }

    // Self destroy after flame hits something
    void OnCollisionEnter2D (Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
