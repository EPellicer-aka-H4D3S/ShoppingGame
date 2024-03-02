using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthManager: MonoBehaviour
{
    public InventoryUI playerInventoriUI;
    private void Update()
    {
        if (input.GetKeyDown(KeyCode.Mouse1) && !Input.GeyKey(KeyCode.Mouse0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physycs,Raycast(ray, out hit))
            {
                InventoryUIElement inventoryElement = hit.collider.GetComponent<InventoryUIElement>();
               if (inventoryElement != null)
               {
                        ItemBasic item = inventoryElement.item;
                    if (item is IteamHealthPotion) 
                    {
                        IncrementarVida((item as ItemHealthPotion).HealthPoints);
                        playerInventoryUI.Inventory.RemoveItem(item);
                    }
               }

            }
        }
    }
    
}
