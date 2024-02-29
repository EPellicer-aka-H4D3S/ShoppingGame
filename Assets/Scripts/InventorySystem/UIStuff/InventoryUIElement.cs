using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIElement: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image Image;
    public TextMeshProUGUI AmountText;

    private Canvas canvas;
    private GraphicRaycaster raycaster;
    private Transform parent;
    private ItemBasic item;
    private InventoryUI inventory;

    public void SetStuff(InventorySlot slot, InventoryUI inventory)
    {
        Image.sprite = slot.Item.ImageUI;
        AmountText.text = slot.Amount.ToString();
        AmountText.enabled = slot.Amount > 1;

        item = slot.Item;
        this.inventory = inventory;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;

        // Start moving object from the beginning!
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        // We need a few references from UI
        if (!canvas)
        {
            canvas = GetComponentInParent<Canvas>();
            raycaster = canvas.GetComponent<GraphicRaycaster>();
        }
        
        // Change parent of our item to the canvas
        transform.SetParent(canvas.transform, true);
        
        // And set it as last child to be rendered on top of UI
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Continue moving object around screen
        transform.localPosition +=
            new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        bool consumable = item is ConsumableItem;

        // Find objects within canvas
        var results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        foreach (var hit in results)
        {
            var nextInventory = hit.gameObject.GetComponent<InventoryUI>();
            var consumer = hit.gameObject.GetComponent<IConsume>();
            //Debug.Log(hit.gameObject.name);

            if((consumer != null) && consumable)
            {
                (item as ConsumableItem).Use(consumer);
                inventory.ItemUsed(item);
            }

            if (nextInventory != null)
            {
                nextInventory.Inventory.AddItem(item);
                inventory.Inventory.RemoveItem(item);

                var nextVal = int.Parse(inventory.GetComponentInChildren<TextMeshProUGUI>().text)-item.price;
                inventory.GetComponentInChildren<TextMeshProUGUI>().SetText(nextVal.ToString());
                nextVal = int.Parse(nextInventory.GetComponentInChildren<TextMeshProUGUI>().text) + item.price;
                nextInventory.GetComponentInChildren<TextMeshProUGUI>().SetText(nextVal.ToString());


                inventory = nextInventory;
            }
        }

        // Changing parent back to slot
        transform.SetParent(parent.transform);

        // And centering item position
        transform.localPosition = Vector3.zero;
    }
}
