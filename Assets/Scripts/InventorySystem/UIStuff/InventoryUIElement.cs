using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
Dear programmer:

When Harlock modified this code to work with all the others codes and the sacred Solid Principles, only god and
I knew how it worked.

Now, only god knows it. The fact that this spaghetti code exists proves that God is either impotent to alter his universe or ignorant to the horrors taking place in his kingdom. This prism of computer data is more than holy bitts. It is a physical declaration of mankind's contempt for the natural order.

Therefore, if you are trying to optimize
this sacrated routine and it fails (most surely),
please increase this holy counter as a
warning for the next benighted person:

total_hours_wasted_here = 254 

May the Omnissiah bless you in his glory
*/


public class InventoryUIElement: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image Image;
    public TextMeshProUGUI AmountText;

    private Canvas canvas;
    private GraphicRaycaster raycaster;
    private Transform parent;
    public ItemBasic item;
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

            if (nextInventory != null)
            {
                nextInventory.Inventory.AddItem(item);
                inventory.Inventory.RemoveItem(item);

                var nextVal = int.Parse(inventory.GetComponentInChildren<TextMeshProUGUI>().text) + item.price;
                inventory.GetComponentInChildren<TextMeshProUGUI>().SetText(nextVal.ToString());
                nextVal = int.Parse(nextInventory.GetComponentInChildren<TextMeshProUGUI>().text) - item.price;
                nextInventory.GetComponentInChildren<TextMeshProUGUI>().SetText(nextVal.ToString());


                inventory = nextInventory;
            }
        }

        // Changing parent back to slot
        transform.SetParent(parent.transform);

        // And centering item position
        transform.localPosition = Vector3.zero;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item is ConsumableItem)
            {   
                ConsumeItem consumeItem = new ConsumeItem();
                consumeItem.Use(item as ConsumableItem);
            }
        }
    }
}
