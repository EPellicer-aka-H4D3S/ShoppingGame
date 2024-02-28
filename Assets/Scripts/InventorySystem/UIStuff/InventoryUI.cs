using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory Inventory;
    public InventoryUIElement ElementPrefab;

    List<GameObject> shownObjects;

    void Start()
    {
        ShowInventory(Inventory);
    }

    private void OnEnable()
    {
        Inventory.OnInventoryChange += UpdateInventory;
    }

    private void OnDisable()
    {
        Inventory.OnInventoryChange -= UpdateInventory;
    }

    private void UpdateInventory()
    {
        ClearInventory();
        ShowInventory(Inventory);
    }

    private void ClearInventory()
    {
        foreach (var item in shownObjects)
        {
            if (item) Destroy(item);
        }

        shownObjects.Clear();
    }

    private void ShowInventory(Inventory inventory)
    {
        if (shownObjects == null) shownObjects = new List<GameObject>();
        if (shownObjects.Count > 0) ClearInventory();

        for (int i = 0; i < inventory.Length; i++)
        {
            shownObjects.Add(MakeNewEntry(inventory.GetSlot(i)));
        }
    }

    private GameObject MakeNewEntry(InventorySlot inventorySlot)
    {
        var element = GameObject.Instantiate(ElementPrefab, Vector3.zero, Quaternion.identity, transform);
        element.SetStuff(inventorySlot, this);
        return element.gameObject;
    }

    public void ItemUsed(ItemBasic item)
    {
        Inventory.RemoveItem(item);
    }
}
