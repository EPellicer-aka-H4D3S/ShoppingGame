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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        healthBar = GetComponentInChildren<Slider>();
    }

    public static HealthManager instance;
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