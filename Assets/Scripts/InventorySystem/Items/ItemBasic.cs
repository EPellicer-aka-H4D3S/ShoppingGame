using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Item")]
public class ItemBasic : ScriptableObject
{
    public string Name;
    public Sprite ImageUI;
    public bool IsStackable;
}
