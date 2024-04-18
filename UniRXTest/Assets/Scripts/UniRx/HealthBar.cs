using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Inject]private  Health targetHealth;
    private Slider healthBar;

    private void Awake()
    {
        healthBar = GetComponent<Slider>();
    }

    private void Update()
    {
        if (targetHealth != null)
        {
            healthBar.value = (float)targetHealth.CurrentHealth / targetHealth.MaxHealth;
        }
    }
}
