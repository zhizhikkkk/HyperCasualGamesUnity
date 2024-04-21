
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using R3;

public class HealthBar : MonoBehaviour
{
    [Inject]private  Health targetHealth;
    private Slider healthBar;
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        targetHealth.healthProperty
            .Subscribe(newHealth =>
            {
                healthBar.value = newHealth / (float)targetHealth.MaxHealth;
            })
            .AddTo(_disposable);

    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}
