
using UnityEngine;
using R3;
using R3.Triggers;
public class Movement : MonoBehaviour
{
    private CompositeDisposable _disposable = new CompositeDisposable();
    public float speed = 20f;
    private Rigidbody _rb;
    private Collider _collider;
    private bool inAir = false;

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
        Observable.EveryUpdate()
            .Where(_=> Input.GetKeyDown(KeyCode.Space) && !inAir)
            .Subscribe(_ =>
            {
                _rb.AddForce(speed * Vector3.up*100);
                inAir = true;
            }).AddTo(_disposable);

        Observable.EveryUpdate()
            .Where(_=>Input.GetKeyDown(KeyCode.LeftControl) )
            .Subscribe(_ =>
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            })
            .AddTo(_disposable);

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyUp(KeyCode.LeftControl))
            .Subscribe(_ =>
            {
                transform.localScale = new Vector3(1, 1, 1);
            })
            .AddTo(_disposable);

        _collider.OnCollisionEnterAsObservable()
            .Where(t =>t.gameObject.tag =="Food" )
           .Subscribe(other =>
           {
               Debug.Log("־גמשü");
               other.transform.position = new Vector3(UnityEngine.Random.Range(0, 10f), 0, UnityEngine.Random.Range(0, 10f));
           })
           .AddTo(_disposable);

        _collider.OnCollisionEnterAsObservable()
            .Where(t => t.gameObject.tag == "Floor")
           .Subscribe(other =>
           {
              inAir = false;
           })
           .AddTo(_disposable);

        _collider.OnCollisionExitAsObservable()
             .Where(t => t.gameObject.tag == "Floor")
           .Subscribe(other =>
           {
               inAir = true;
           })
           .AddTo(_disposable);

    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}
