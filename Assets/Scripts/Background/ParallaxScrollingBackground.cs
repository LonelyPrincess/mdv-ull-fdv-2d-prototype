using UnityEngine;
using UnityEngine.Rendering;

public class ParallaxScrollingBackground : MonoBehaviour
{
    // Base movement speed (final value will depend on layer depth)
    public float speed = 0.5f;

    // List of children background renderers found inside current game object
    Renderer[] layerRenderers;

    void Start ()
    {
        layerRenderers = GetComponentsInChildren<Renderer>();
        Debug.Log("Background contains " + layerRenderers.Length + " layers");
    }

    void Update ()
    {
        foreach (Renderer layer in layerRenderers) {
            SortingGroup sortGroup = layer.gameObject.GetComponent<SortingGroup>();
            float layerSpeed = sortGroup.sortingOrder * speed;

            Debug.Log(layer.gameObject.name + " should move at speed " + layerSpeed);

            // Apply texture offset to simulate movement
            layer.material.mainTextureOffset += new Vector2(layerSpeed * Time.deltaTime, 0);
        }
    }
}
