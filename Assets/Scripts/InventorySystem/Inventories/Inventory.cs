using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField]
    List<InventorySlot> Slots;

    public int Length => Slots.Count;

    public delegate void InventoryChangeDelegate();
    public InventoryChangeDelegate OnInventoryChange;

    public void AddItem(ItemBasic item)
    {
        if (Slots == null) Slots = new List<InventorySlot>();

        var slot = GetSlot(item);

        if (slot != null && item.IsStackable)
        {
            slot.AddOne();
        }
        else
        {
            slot = new InventorySlot(item);
            Slots.Add(slot);
        }

        OnInventoryChange?.Invoke();
    }

    public void RemoveItem(ItemBasic item)
    {
        if (Slots == null) return;

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
        Slots.Remove(slot);
    }

    private InventorySlot GetSlot(ItemBasic item)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].HasItem(item)) 
                return Slots[i];
        }

        return null;
    }

    public InventorySlot GetSlot(int i)
    {
        return Slots[i];
    }
}
