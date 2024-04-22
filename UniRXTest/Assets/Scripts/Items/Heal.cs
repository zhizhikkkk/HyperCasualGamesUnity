using R3.Triggers;
using UnityEngine;
using R3;

public class Heal : IItem
{
    public InventItem itemToAdd;
    private void Start()
    {
        _trigger = GetComponent<Collider>();
        _trigger.OnCollisionEnterAsObservable()
            .Where(_ => _.gameObject.tag == "Player")
            .Subscribe(_ =>
            {
                Destroy(this.gameObject);
                inventory.AddItem(itemToAdd);
            })
            .AddTo(_disposable);
    }
    private void OnDisable()
    {
        _disposable.Clear();
    }
}
