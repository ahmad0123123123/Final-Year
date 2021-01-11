

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool _useInput;
    
    [SerializeField]
    public static bool useInput;
    
    [Header("Movement keys")]
    public KeyCode Crouch;
    public KeyCode Run;
    public KeyCode Jump;
    public KeyCode LeanLeft;
    public KeyCode LeanRight;

    [Header("Gameplay keys")]
    public KeyCode Fire;
    public KeyCode Aim;
    public KeyCode Use;
    public KeyCode DropWeapon;
    public KeyCode Reload;
    public KeyCode FiremodeSingle;
    public KeyCode FiremodeAuto;
    public KeyCode Inventory;

    public static Vector2 joystickInputVector;
    public static Vector2 touchPanelLook;

    private void Awake()
    {
        useInput = _useInput;
    }
}
