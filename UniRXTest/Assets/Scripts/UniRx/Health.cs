using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using TMPro;


public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private TextMeshProUGUI healthTxt;
    public ReactiveProperty<int> healthProperty;
    public int CurrentHealth => health;
    public int MaxHealth => 100;
    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Awake()
    {
        healthProperty= new ReactiveProperty<int>(health);

        healthProperty
            .Subscribe(newHealth =>
            {
                healthTxt.text = newHealth.ToString();
                health = newHealth;
            })
            .AddTo(_disposable);
    }

    public void TakeDamage(int damage)
    {
        healthProperty.Value = Mathf.Max(healthProperty.Value - damage, 0);
    }

    public void Heal(int amount)
    {
        healthProperty.Value = Mathf.Min(healthProperty.Value + amount, MaxHealth);
    }

    public void OnDisable()
    {
        _disposable.Clear();
    }
}
