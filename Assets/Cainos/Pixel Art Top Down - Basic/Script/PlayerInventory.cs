using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public void EquipWeapon(ItemData weapon)
    {
        Debug.Log($"🛡️ Equipped: {weapon.itemName}");
        // Aktifkan pedang, ganti sprite, dll
    }

    public void ActivateRelic(ItemData relic)
    {
        Debug.Log($"✨ Relic activated: {relic.itemName}");
        // Misal: tambah speed, score, efek partikel
    }

    public void UsePotion(ItemData potion)
    {
        Debug.Log($"❤️ Potion used: {potion.itemName}");
        // Tambah HP, efek heal, dsb
    }
}
