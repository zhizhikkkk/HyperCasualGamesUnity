
using UnityEngine;
using R3;
using Zenject;


public class IItem : MonoBehaviour
{
    protected Collider _trigger;
    protected CompositeDisposable _disposable = new CompositeDisposable();
    [Inject] public Health health;
    [Inject] public Score score;
    [Inject] public InventoryWindow inventoryWindow;
    [Inject] public Inventory inventory;

    
}
