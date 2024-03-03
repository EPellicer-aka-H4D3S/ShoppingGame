using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class InventorySlot
{
    public int Amount => amount;
    public ItemBasic Item => item;

    [SerializeField]
    private ItemBasic item;
    [SerializeField]
    private int amount;

    public InventorySlot(ItemBasic item)
    {
        this.item = item;
        amount = 1;
    }

    internal bool HasItem(ItemBasic item)
    {
        return item == this.item;
    }

    internal bool CanHold(ItemBasic item)
    {
        if (item.isStackable) return (item == this.item);

        return false;
    }

    internal void AddOne()
    {
        amount++;
    }

    internal void RemoveOne()
    {
        amount--;
    }

    public bool IsEmpty()
    {
        return amount < 1;
    }
}