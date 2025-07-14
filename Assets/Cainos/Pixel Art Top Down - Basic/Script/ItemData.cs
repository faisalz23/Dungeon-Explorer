using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    [TextArea]
    public string description;

    public enum ItemType { Weapon, Relic, Potion }
    public ItemType itemType;

    public void Use(GameObject player)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                Debug.Log($"🗡️ {"pedang"} picked up - weapon equipped!");
                player.GetComponent<PlayerInventory>()?.EquipWeapon(this);
                break;

            case ItemType.Relic:
                Debug.Log($"🔮 {itemName} picked up - relic effect triggered!");
                player.GetComponent<PlayerInventory>()?.ActivateRelic(this);
                break;

            case ItemType.Potion:
                Debug.Log($"🍷 {itemName} picked up - potion effect triggered!");
                player.GetComponent<PlayerInventory>()?.UsePotion(this);
                break;
        }
    }
}
