using R3.Triggers;
using UnityEngine;
using R3;

public class Food : IItem
{
    public InventItem itemToAdd; 
    private void Start()
    {
        _trigger = GetComponent<Collider>();
        _trigger.OnCollisionEnterAsObservable()
            .Where(_ => _.gameObject.tag == "Player")
            .Subscribe(_ =>
            {
                inventory.AddItem(itemToAdd);
                Destroy(this.gameObject);
                score.AddScore(1);
                
            })
            .AddTo(_disposable);
    }
    private void OnDisable()
    {
        _disposable.Clear();
    }
}
