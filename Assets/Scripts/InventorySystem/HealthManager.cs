using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthManager: MonoBehaviour
{
    private Slider healthBar;
    private void Start()
    {
        healthBar = GetComponentInChildren<Slider>();

        if (healthBar == null)
        {
            Debug.LogError("No se encontró el slider de salud (HealthBar) en el Canvas.");
        }
    }
    public void ConsumeItem(ConsumableItem consumableItem)
    {
        if (consumableItem is ItemHealthPotion)
        {
            int healthPoints = (consumableItem as ItemHealthPotion).HealthPoints;
            UpdateHealthSlider(healthPoints);
            Debug.Log(healthPoints);
        }
        else
        {

        }
    }
     private void UpdateHealthSlider(int healthPoints)
    {
        healthBar.value += healthPoints;
    }

    public void Use(ConsumableItem consumableItem)
    {
        ConsumeItem(consumableItem);
    }

    internal static void ConsumeItem(ItemBasic item)
    {
        throw new NotImplementedException();
    }
}
