using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Amount of seconds that must pass before attempting to re-spawn items
    public float minTimeToRespawn = 5f;
    public float maxTimeToRespawn = 10f;

    float timeToRespawn = 0;

    // Time ellapsed since last time a spawn took place
    float timeSinceLastSpawn = 0;

    // Range of coordinates in the horizontal axis where the items will spawn
    public float minSpawnLocationX = -9;
    public float maxSpawnLocationX = 5;
    public float minSpawnLocationY = 3;
    public float maxSpawnLocationY = 5;

    // Amount of items that have been generated over time
    int generatedItemCount = 0;

    void Start () {
        // Subscribe to pick up events
        Item.OnPickUp += DestroyItem;
    }

    void Update () {
        timeSinceLastSpawn += Time.deltaTime;

        // Respawn items after enough time has passed
        if (timeSinceLastSpawn > timeToRespawn) {
            SpawnItems();
            timeSinceLastSpawn = 0;
            RecalculateRespawnTime();
        }
    }

    void RecalculateRespawnTime () {
        timeToRespawn = Random.Range(minTimeToRespawn, maxTimeToRespawn);
        Debug.LogWarning("Next respawn will trigger in " + timeToRespawn + " seconds");
    }

    // Keep spawning items until pool is full
    void SpawnItems ()
    {
        while (GenerateItem () != null) {
            generatedItemCount += 1;
        }
    }

    // Attempt to generate a new item in a random position
    //  (only if there's available space in the object pool)
    GameObject GenerateItem () {
        GameObject item = ObjectPool.SharedInstance.GetPooledObject();
        if (item != null) {
            item.name = "Item" + (generatedItemCount + 1);
            item.transform.position = new Vector2(
                Random.Range(minSpawnLocationX, maxSpawnLocationX),
                Random.Range(minSpawnLocationY, maxSpawnLocationY));
            item.SetActive(true);
        }

        return item;
    }

    // Disable object in pool
    void DestroyItem (Item item, GameObject itemPicker) {
        item.gameObject.SetActive(false);
    }
}
