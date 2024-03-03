using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Item")]
public class ItemBasic : ScriptableObject
{
    public string itemName;
    public Sprite imageUI;
    public bool isStackable;
    public int price;
}