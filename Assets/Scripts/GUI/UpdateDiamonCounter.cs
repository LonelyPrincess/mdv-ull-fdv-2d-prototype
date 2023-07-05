using UnityEngine;
using TMPro;

public class UpdateDiamonCounter : MonoBehaviour
{
    float collectedItemCount = 0;
    public TextMeshProUGUI countText;

    void Start ()
    {
        Item.OnPickUp += OnItemPickUp;
        RefreshCountInUI();
    }

    void RefreshCountInUI () {
        countText.SetText(collectedItemCount.ToString());
    }

    void OnItemPickUp (Item item, GameObject itemPicker) {
        collectedItemCount += 1;
        RefreshCountInUI();
    }
}
