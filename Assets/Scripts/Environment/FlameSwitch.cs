using UnityEngine;
using TMPro;

public class FlameSwitch : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject flamePrefab;

    public int flamePrice = 5;
    public TextMeshProUGUI flamePriceText;
    public GemManager gemManager;

    void Start () {
        flamePriceText.SetText(flamePrice.ToString());
    }

    // Spawn a flame that will move left until it hits something
    void ShootFlame () {
        GameObject flame = Instantiate(flamePrefab, spawnPoint.transform);
        Rigidbody2D rb = flame.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * 2.0f;
    }

    // Instantiate a new flame when the player touches the switch (if they have enough money)
    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && gemManager.GetAvailableGemCount() >= flamePrice) {
            gemManager.ConsumeGems(flamePrice);
            ShootFlame();
        }
    }
}
