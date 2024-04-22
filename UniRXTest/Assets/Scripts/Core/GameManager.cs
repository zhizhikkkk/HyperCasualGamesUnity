using UnityEngine;
using R3;
using R3.Triggers;
using Zenject;
using System;
public class GameManager : MonoBehaviour
{
    [Inject] public InventoryWindow inventoryWindow;
    private CompositeDisposable _disposable = new CompositeDisposable();
    public void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Tab) )
            .Subscribe(_ =>
            {
                inventoryWindow.itemsPanel.gameObject.SetActive(!inventoryWindow.itemsPanel.gameObject.activeSelf);
            }).AddTo(_disposable);
    }
            
}
