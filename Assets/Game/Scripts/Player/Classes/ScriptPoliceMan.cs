using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class ScriptPoliceMan: ClassBase
{
    private InputSestem _actions;

    private void Start()
    {        
        _actions = new InputSestem();
        _actions.Enable();
        foreach (Transform transformObj in GetComponentsInChildren<Transform>())
        {
            if (transformObj.tag == "Picker")
            {
                _pickerObject = transformObj;
            }
        }
    }

    private void Update()
    {
        if(CheckItem() == true && _actions.Player.Interact.WasPressedThisFrame())
        {
            GetItem();
        }
    }
}