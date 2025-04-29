using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _mouseSensivity = 2f;
    
    float _rotationY = 0f;
    float _rotationX = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * _mouseSensivity;
        float inputY = Input.GetAxis("Mouse Y") * _mouseSensivity;

        _rotationY -= inputY;
        _rotationX += inputX;

        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);
        Camera.main.transform.position = transform.position;
        Camera.main.transform.localRotation = Quaternion.Euler(_rotationY, 0, 0);

        transform.Rotate(Vector3.up * inputX);
    }
}
