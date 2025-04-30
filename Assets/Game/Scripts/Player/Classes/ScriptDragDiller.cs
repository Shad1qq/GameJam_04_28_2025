using Photon.Pun;
using UnityEngine;

public class ScriptDragDiller:ClassBase
{
    private InputSestem _actions;
    private PhotonView photonViewe;
    private SpawnPlayer sp;

    public GameObject Picker;

    private void Start()
    {
        sp = (SpawnPlayer)FindObjectOfType(typeof(SpawnPlayer));
        photonViewe = sp.GetComponent<PhotonView>();

        Picker = transform.Find("Picker").gameObject;
        _actions = new InputSestem();
        _actions.Enable();
    }

    private void Update()
    {
        if (CheckItem() == true && _actions.Player.pic.WasPressedThisFrame()){
            sp._pickerObject.position = Picker.transform.position;

            photonViewe.RPC("GetItem", RpcTarget.All, Picker.GetComponent<PhotonView>().ViewID);
        }

        if (transform.childCount > 1 && _actions.Player.Attack.WasPressedThisFrame())
            photonViewe.RPC("DropItem", RpcTarget.All);
    }
}