using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/PoisionPotion")]
public class ItemPoisonPotion : ConsumableItem
{
    public int PoisonPoints;

    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}
