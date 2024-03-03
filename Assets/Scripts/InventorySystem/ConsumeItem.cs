using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItem : MonoBehaviour, IConsume
{
  public void Use(ConsumableItem item)
    {
        HealthManager healthManager = FindObjectOfType<HealthManager>();

        if (item is ItemHealthPotion)
        {
            int healthPoints = (item as ItemHealthPotion).HealthPoints;
            healthManager.ApplyHealth(healthPoints);
        }
        else if (item is ItemPoisonPotion)
        {
            int poisonPoints = (item as ItemPoisonPotion).PoisonPoints;
            healthManager.ApplyDamage(poisonPoints);
        }
        else
        {
           // si hay otros consumibles, aqui 
        }
    }
}



