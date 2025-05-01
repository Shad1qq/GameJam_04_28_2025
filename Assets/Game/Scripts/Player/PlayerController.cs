using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private Rigidbody _rb;
    private PhotonView _view;
    [SerializeField] private float _speed;

    [SerializeField]private float _jumpForce;
    private InputSestem _inputActions;

    [System.Obsolete]
    private void Start()
    {
        _inputActions = FindObjectOfType<SpawnPlayer>()._inputActions;

        _rb = GetComponent<Rigidbody>();
        _view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        Move();
        if (_inputActions.Player.Jump.WasPressedThisFrame())
        {
            Jump();
        }
    }

    [PunRPC]
    private void Move()
    {
        if (_view.IsMine)
        {
            Vector3 direction = _inputActions.Player.Move.ReadValue<Vector3>();
            Vector3 worldPos = transform.TransformDirection(direction) * _speed;
            _rb.linearVelocity = new Vector3(worldPos.x * _speed, _rb.linearVelocity.y, worldPos.z * _speed);
        }      
    }

    [PunRPC]
    private void Jump()
    {
        if (_view.IsMine)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

    }
}
