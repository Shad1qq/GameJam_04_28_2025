using Photon.Pun;
using UnityEngine;

public class ScriptPoliceMan: ClassBase
{
    private InputSestem _actions;
    private PhotonView photonViewe;

    [System.Obsolete]
    private void Start()
    {        
        SpawnPlayer sp = (SpawnPlayer)FindObjectOfType(typeof(SpawnPlayer));
        photonViewe = sp.GetComponent<PhotonView>();

        _actions = FindObjectOfType<SpawnPlayer>()._inputActions;
    }

    private void Update()
    {
        if(CheckItem() == true && _actions.Player.Attack.WasPressedThisFrame()){
            photonViewe.RPC("PanelDis", RpcTarget.All);
            //победное окно
        }

        // if (_pickerObject.childCount > 0 && _actions.Player.Jump.WasPressedThisFrame())
        //     photonView.RPC("DropItem", RpcTarget.All, ID);
    }
}