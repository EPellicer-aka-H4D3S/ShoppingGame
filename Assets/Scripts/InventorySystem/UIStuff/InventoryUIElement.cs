﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIElement: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image image;
    public TextMeshProUGUI amountText;
    public ItemBasic item;

    private Canvas canvas;
    private GraphicRaycaster raycaster;
    private Transform parent;
    private InventoryUI inventory;

    public void SetStuff(InventorySlot slot, InventoryUI inventory)
    {
        image.sprite = slot.Item.imageUI;
        amountText.text = slot.Amount.ToString();
        amountText.enabled = slot.Amount > 1;

        item = slot.Item;
        this.inventory = inventory;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);

        if (!canvas)
        {
            canvas = GetComponentInParent<Canvas>();
            raycaster = canvas.GetComponent<GraphicRaycaster>();
        }
        
        transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition +=
            new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        bool consumable = item is ConsumableItem;
        var results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        foreach (var hit in results)
        {
            var nextInventory = hit.gameObject.GetComponent<InventoryUI>();
            var consumer = hit.gameObject.GetComponent<IConsume>();
            //Debug.Log(hit.gameObject.name);

            if (nextInventory != null)
            {
                nextInventory.inventory.AddItem(item);
                inventory.inventory.RemoveItem(item);

                var nextVal = int.Parse(inventory.GetComponentInChildren<TextMeshProUGUI>().text) + item.price;
                inventory.GetComponentInChildren<TextMeshProUGUI>().SetText(nextVal.ToString());
                nextVal = int.Parse(nextInventory.GetComponentInChildren<TextMeshProUGUI>().text) - item.price;
                nextInventory.GetComponentInChildren<TextMeshProUGUI>().SetText(nextVal.ToString());

                inventory = nextInventory;
            }
        }

        transform.SetParent(parent.transform);
        transform.localPosition = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item is ConsumableItem&&inventory.gameObject.name == "InventoryPlayer")
            {
                inventory.inventory.RemoveItem(item);
                ConsumeItem consumeItem = gameObject.AddComponent<ConsumeItem>();
                consumeItem.Use(item as ConsumableItem);
            }
        }
    }
}