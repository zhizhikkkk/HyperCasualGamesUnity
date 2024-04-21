using R3.Triggers;
using UnityEngine;
using R3;

public class Food : IItem
{
    private void Start()
    {
        _trigger = GetComponent<Collider>();
        _trigger.OnCollisionEnterAsObservable()
            .Where(_ => _.gameObject.tag == "Player")
            .Subscribe(_ =>
            {
                score.AddScore(1);
                Destroy(this.gameObject);
            })
            .AddTo(_disposable);
    }
    private void OnDisable()
    {
        _disposable.Clear();
    }
}
