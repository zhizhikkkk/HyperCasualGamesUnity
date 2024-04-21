
using UnityEngine;
using R3;

public class CameraPlayer : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public Transform player;
    float xRotation = 0;
    private CompositeDisposable _disposable = new CompositeDisposable();

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Observable.EveryUpdate()
            .Select(_=> new Vector3(Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime, 0, Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime))
            .Where(input => input != null)
            .Subscribe(input =>
            {
                xRotation -= input.z;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
                player.Rotate(Vector3.up * input.x);

            })
            .AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

}
