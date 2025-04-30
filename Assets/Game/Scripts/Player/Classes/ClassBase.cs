using Photon.Pun;
using UnityEngine;
public abstract class ClassBase : MonoBehaviourPunCallbacks
{
    [SerializeField]protected float _maxDistance = 4f;    

    public bool CheckItem()
    {
        Debug.DrawLine(transform.position, Camera.main.transform.forward * _maxDistance);
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit _hit, _maxDistance, 1<<7))
            return true;
        return false;
    }
}

