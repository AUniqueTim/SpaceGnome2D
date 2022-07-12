using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    public float maxBoost;
    public float currentBoost;
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }
    public void ReceiveDrink(int hpRecovered)
    {
        currentHP += hpRecovered;
       
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
    }
    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
    }
    public void LoseBoost(int lostAmount)
    {
        currentBoost -= lostAmount;
        if (currentBoost >= maxBoost)
        {
            currentBoost = maxBoost;
        }
    }
}
