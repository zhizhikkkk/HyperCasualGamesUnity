
using UnityEngine;
using R3;
using R3.Triggers;

public class Damage :  IItem
{
    private void Start()
    {
        _trigger = GetComponent<Collider>();
        _trigger.OnCollisionEnterAsObservable()
            .Where(_ => _.gameObject.tag == "Player")
            .Subscribe(_ =>
            {
                Destroy(this.gameObject);
                health.TakeDamage(20);

            })
            .AddTo(_disposable);
    }
    private void OnDisable()
    {
        _disposable.Clear();
    }
}
