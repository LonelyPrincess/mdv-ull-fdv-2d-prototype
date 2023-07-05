using UnityEngine;

public class Flame : MonoBehaviour
{
    float minScale = 0.5f;
    float maxScale = 2.0f;
    float currentScale = 1.0f;
    bool isFlameGrowing = true;

    // Update is called once per frame
    void Update()
    {
        ResizeFlame();
        this.transform.Rotate(new Vector3(0, 0, 1.0f));
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

    // Self destroy after flame hits something (excluding collectibles)
    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag != "Collectible") {
            Destroy(this.gameObject);
        }
    }
}
