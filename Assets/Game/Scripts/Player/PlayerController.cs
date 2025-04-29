using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]private float _speed;

    private InputSestem _inputActions;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _inputActions = new InputSestem();
        _inputActions.Enable();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        Vector3 direction = _inputActions.Player.Move.ReadValue<Vector3>();
        _rb.linearVelocity = new Vector3(direction.x * _speed, _rb.linearVelocity.y, direction.z * _speed);
    }
    private void OnDestroy()
    {
        _inputActions.Disable();
    }
}
