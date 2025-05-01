using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviourPunCallbacks
{
    [SerializeField] private float _mouseSensivity = 2f;

    private PhotonView _view;
    
    float _rotationY = 0f;
    float _rotationX = 0f;
    
    private void Start()
    {
        _view = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (_view.IsMine)
            Camera.main.transform.SetParent(transform);

        Camera.main.transform.position = _view.transform.position;
    }

    private void Update()
    {
        if (_view.IsMine)
        {
            float inputX = Input.GetAxis("Mouse X") * _mouseSensivity;
            float inputY = Input.GetAxis("Mouse Y") * _mouseSensivity;

            _rotationY -= inputY;
            _rotationX += inputX;

            _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);
            
            Camera.main.transform.localRotation = Quaternion.Euler(_rotationY, 0, 0);

            _view.transform.Rotate(Vector3.up * inputX);
        }
    }
}
