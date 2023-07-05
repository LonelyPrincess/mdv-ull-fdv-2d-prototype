using UnityEngine;
using TMPro;

public class GemManager : MonoBehaviour
{
    int collectedGemCount = 0;
    public TextMeshProUGUI countText;

    void Start ()
    {
        Item.OnPickUp += OnGemPickUp;
        RefreshCountInUI();
    }

    void RefreshCountInUI () {
        countText.SetText(collectedGemCount.ToString());
    }

    void OnGemPickUp (Item item, GameObject itemPicker) {
        collectedGemCount += 1;
        RefreshCountInUI();
    }

    public int GetAvailableGemCount () {
      return collectedGemCount;
    }

    public void ConsumeGems (int amount) {
        collectedGemCount -= amount;
        RefreshCountInUI();
    }
}
