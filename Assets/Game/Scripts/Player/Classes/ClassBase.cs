using UnityEngine;
public abstract class ClassBase:MonoBehaviour
{
    [SerializeField]protected float _maxDistance = 4f;
    [SerializeField]protected Transform _pickerObject;
    private RaycastHit _hit;

    public bool CheckItem()
    {
        Debug.DrawLine(transform.position, Camera.main.transform.forward * _maxDistance);
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out _hit, _maxDistance, 1<<7))
        {
            return true;
        }
        return false;
    }
    public void GetItem()
    {
        _hit.transform.position = _pickerObject.transform.position;
        _hit.transform.SetParent(_pickerObject.transform);
    }
}

