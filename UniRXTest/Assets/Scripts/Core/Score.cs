
using TMPro;
using UnityEngine;
using R3;

public class Score : MonoBehaviour
{
    private CompositeDisposable _disposable = new CompositeDisposable();
    public ReactiveProperty<int> score = new ReactiveProperty<int>(0);
    [SerializeField] private TextMeshProUGUI scoreTxt;
    
    private void Start()
    {
        score.Subscribe(newScore =>
        {
            scoreTxt.text = newScore.ToString();
        }).AddTo(_disposable);
    }
    public void AddScore(int amount)
    {
        score.Value += amount;
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}
