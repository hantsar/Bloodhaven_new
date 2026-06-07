using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    private Player player;
    public List<Inventory_EquipmentSlot> equipList;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    public void TryEquipItem(Inventory_Item item)
    {
        var inventoryItem = FindItem(item.itemData);
        var matchingSlots = equipList.FindAll(slot => slot.slotType == item.itemData.itemType);

        // STEP 1 : Try to find empty slot and equip item
        foreach (var slot in matchingSlots)
        {
            if (slot.HasItem() == false)
            {
                EquipItem(inventoryItem, slot);
                return;
            }
        }

        // STEP 2: No empty slots ? Replace first one
        var slotToReplace = matchingSlots[0];
        var itemToUneqip = slotToReplace.equipedItem;

        UnequipItem(itemToUneqip, slotToReplace != null);
        EquipItem(inventoryItem, slotToReplace);
    }

    private void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot)
    {
        float savedHealthPercent = player.health.GetHealthPercent();

        slot.equipedItem = itemToEquip;
        slot.equipedItem.AddModifiers(player.stats);

        player.health.SetHealthToPercent(savedHealthPercent);
        RemoveItem(itemToEquip);
    }

    public void UnequipItem(Inventory_Item itemToUnequip, bool replacingItem = false)
    {
        if (CanAddItem() == false && replacingItem == false)
        {
            Debug.Log("No space!");
            return;
        }

        float savedHealthPercent = player.health.GetHealthPercent();
        var slotToUnequip = equipList.Find(slot => slot.equipedItem == itemToUnequip);

        if (slotToUnequip != null)
            slotToUnequip.equipedItem = null;

        itemToUnequip.RemoveModifiers(player.stats);

        player.health.SetHealthToPercent(savedHealthPercent);
        AddItem(itemToUnequip);
    }
}
