
using UnityEngine;
using R3;
using R3.Triggers;
public class Movement : MonoBehaviour
{
    private CompositeDisposable _disposable = new CompositeDisposable();
    public float speed = 20f;
    private Rigidbody _rb;
    private Collider _collider;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
                transform.Translate(movement * speed * Time.deltaTime);
            })
            .AddTo(_disposable);

        _collider.OnCollisionEnterAsObservable()
            .Where(t =>t.gameObject.tag =="Food" )
           .Subscribe(other =>
           {
               Debug.Log("־גמשü");
               Destroy(other.gameObject);
           })
           .AddTo(_disposable);

    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}
