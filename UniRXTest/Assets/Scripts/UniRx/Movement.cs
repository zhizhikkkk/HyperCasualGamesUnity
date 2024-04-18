
using UnityEngine;
using R3;
using R3.Triggers;
using TMPro;
using Zenject;
public class Movement : MonoBehaviour
{
    private CompositeDisposable _disposable = new CompositeDisposable();
    private float speed = 10f;
    private Rigidbody _rb;
    private Collider _collider;
    private bool inAir = false;
    private float sprint = 1f;
    private int score=0;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [Inject] private Health health;

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
                _rb.AddForce(speed * Vector3.up*10);
                inAir = true;
            }).AddTo(_disposable);

        Observable.EveryUpdate()
            .Where(_=>Input.GetKeyDown(KeyCode.LeftControl) )
            .Subscribe(_ =>
            {
                transform.parent.localScale = new Vector3(1f, 0.5f, 1f);
            })
            .AddTo(_disposable);

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyUp(KeyCode.LeftControl))
            .Subscribe(_ =>
            {
                transform.parent.localScale = new Vector3(1, 1, 1);
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
               //other.transform.position = new Vector3(Random.Range(0, 20f), 0, Random.Range(0, 20f));
               Destroy(other.gameObject);
               score++;
               scoreTxt.text = score.ToString();
           })
           .AddTo(_disposable);



        _collider.OnCollisionEnterAsObservable()
            .Where(t => t.gameObject.tag == "Heal")
           .Subscribe(other =>
           {
               health.Heal(20);
               Destroy(other.gameObject);
               Debug.Log(health.CurrentHealth);
           })
           .AddTo(_disposable);

        _collider.OnCollisionEnterAsObservable()
            .Where(t => t.gameObject.tag == "Damage")
           .Subscribe(other =>
           {
               health.TakeDamage(20);
               Destroy(other.gameObject);
               Debug.Log(health.CurrentHealth);
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
