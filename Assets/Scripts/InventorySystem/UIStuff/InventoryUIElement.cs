﻿using System.Collections.Generic;
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
        // Find objects within canvas
        var results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        foreach (var hit in results)
        {
            Debug.Log(hit.gameObject.name);
        }

        // Find scene objects            
        RaycastHit2D hitData = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hitData)
        {
            var inventory = hitData.collider.GetComponent<InventoryUI>();
            var consumer = hitData.collider.GetComponent<IConsume>();
            bool consumable = item is ConsumableItem;

            if ((consumer != null) && consumable)
            {
                (item as ConsumableItem).Use(consumer);
                inventory.ItemUsed(item);
            }

            if (inventory != null)
            {
                Debug.Log(inventory);
                transform.SetParent(inventory.GetComponentInParent<Transform>());
            }
        }

        // Changing parent back to slot
        transform.SetParent(parent.transform);

        // And centering item position
        transform.localPosition = Vector3.zero;
    }
}
