using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public int CurrentHealth => health;
    public int MaxHealth => 100;

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(health - damage, 0);
    }

    public void Heal(int amount)
    {
        health+= amount;
    }
}
