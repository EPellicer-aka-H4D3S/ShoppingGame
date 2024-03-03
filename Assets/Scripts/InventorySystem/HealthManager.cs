using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        healthBar = GetComponentInChildren<Slider>();
    }
    public static HealthManager Instance;

    private Slider healthBar;
    public void ApplyHealth(int healthPoints)
    {
        healthBar.value += healthPoints;
    }
    public void ApplyDamage(int damagePoints)
    {
        healthBar.value -= damagePoints;
    }
}



    /*
    private Slider healthBar;
    private void Start()
    {
        healthBar = GetComponentInChildren<Slider>();
    }
    public void ConsumeItem(ConsumableItem consumableItem)
    {
        if (consumableItem is ItemHealthPotion)
        {
            int healthPoints = (consumableItem as ItemHealthPotion).HealthPoints;
            UpdateHealthSlider(healthPoints);
            Debug.Log(healthPoints);
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

    */

