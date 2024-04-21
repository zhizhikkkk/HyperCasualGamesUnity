
using UnityEngine;
using R3;
using Zenject;


public class IItem : MonoBehaviour
{
    protected Collider _trigger;
    protected CompositeDisposable _disposable = new CompositeDisposable();
    [Inject] protected Health health;
    [Inject] protected Score score;
    [Inject] protected InventoryWindow inventoryWindow;

    
}
