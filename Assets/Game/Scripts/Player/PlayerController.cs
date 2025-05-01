using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private Rigidbody _rb;
    private PhotonView _view;
    [SerializeField] private float _speed;

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
}
