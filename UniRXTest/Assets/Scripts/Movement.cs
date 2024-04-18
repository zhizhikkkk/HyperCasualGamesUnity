
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
    private float sprint = 1f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        Observable.EveryUpdate()
            .Select(_=>new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")))
            .Where(input=>input!=Vector3.zero)
            .Subscribe(input =>
            {
                Vector3 movement = new Vector3(input.x, 0, input.z);
                transform.Translate(movement * speed * sprint * Time.deltaTime);
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
                transform.localScale = new Vector3(1f, 0.5f, 1f);
            })
            .AddTo(_disposable);

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyUp(KeyCode.LeftControl))
            .Subscribe(_ =>
            {
                transform.localScale = new Vector3(1, 1, 1);
            })
            .AddTo(_disposable);

        Observable.EveryUpdate()
           .Subscribe(_ =>
           {
               sprint = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;
           })
           .AddTo(_disposable);


        _collider.OnCollisionEnterAsObservable()
            .Where(t =>t.gameObject.tag =="Food" )
           .Subscribe(other =>
           {
               Debug.Log("־גמשü");
               other.transform.position = new Vector3(Random.Range(0, 10f), 0, Random.Range(0, 10f));
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
