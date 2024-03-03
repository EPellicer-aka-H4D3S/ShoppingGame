using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField]
    List<InventorySlot> slots;

    public int Length => slots.Count;

    public delegate void InventoryChangeDelegate();
    public InventoryChangeDelegate OnInventoryChange;

    public void AddItem(ItemBasic item)
    {
        if (slots == null) slots = new List<InventorySlot>();

        var slot = GetSlot(item);

        if (slot != null && item.isStackable)
        {
            slot.AddOne();
        }
        else
        {
            slot = new InventorySlot(item);
            slots.Add(slot);
        }

        OnInventoryChange?.Invoke();
    }

    public void RemoveItem(ItemBasic item)
    {
        if (slots == null) return;

        var slot = GetSlot(item);

        if (slot != null)
        {
            slot.RemoveOne();
            if (slot.IsEmpty())
                RemoveSlot(slot);
        }

        OnInventoryChange?.Invoke();
    }

    private void RemoveSlot(InventorySlot slot)
    {
        slots.Remove(slot);
    }

    private InventorySlot GetSlot(ItemBasic item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].HasItem(item)) 
                return slots[i];
        }

        return null;
    }

    public InventorySlot GetSlot(int i)
    {
        return slots[i];
    }
}