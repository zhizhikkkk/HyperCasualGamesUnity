using R3.Triggers;
using UnityEngine;
using R3;

public class Heal : IItem
{
    private void Start()
    {
        _trigger = GetComponent<Collider>();
        _trigger.OnCollisionEnterAsObservable()
            .Where(_ => _.gameObject.tag == "Player")
            .Subscribe(_ =>
            {
                health.Heal(20);
                Destroy(this.gameObject);
            })
            .AddTo(_disposable);
    }
    private void OnDisable()
    {
        _disposable.Clear();
    }
}
