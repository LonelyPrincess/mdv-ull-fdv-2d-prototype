using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSwitch : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject flamePrefab;

    // Instantiate a new flame when the player touches the switch
    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") {
            Debug.Log("should create flame");
            GameObject flame = Instantiate(flamePrefab, spawnPoint.transform);

            Rigidbody2D rb = flame.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.left * 2.0f;
        }
    }

    /*void OnTriggerStay2D (Collider2D collider)
    {
        Debug.Log(collider.gameObject.tag + " is colliding with switch");
        if (collider.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("should create flame");
            Flame flame = Instantiate(flamePrefab, spawnPoint.transform);

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.left * 2.0f;
        }
    }*/
}
