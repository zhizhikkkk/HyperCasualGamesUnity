
using UnityEngine;
using R3;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public Transform player;
    float xRotation = 0;
    private CompositeDisposable _disposable = new CompositeDisposable();

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y")* mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
                player.Rotate(Vector3.up * mouseX);

            })
            .AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

}
