using UnityEngine;
public abstract class ClassBase:MonoBehaviour
{
    [SerializeField]protected float _maxDistance = 4f;    
    [SerializeField] protected float _dropImpulse = 5f;
    protected RaycastHit _hit;
    protected Transform _pickerObject;

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
        _hit.transform.localRotation = Quaternion.Euler(0, 0, 0);
        _hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void DropItem()
    {
        Transform child;
        child = _pickerObject.transform.GetChild(0);
        child.parent = null;
        child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        child.gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * _dropImpulse, ForceMode.Impulse);
    }
}

