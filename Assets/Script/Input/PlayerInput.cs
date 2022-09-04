using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject,PlayerControl.IGamePlayActions
{
    PlayerControl playerControl;
    public event UnityAction<Vector2> onMove;
    public event UnityAction onStopMove;

    public event UnityAction onJump;

    public event UnityAction onStealth;
    //public event UnityAction onStopStealth;

    public event UnityAction onDash;

    public event UnityAction onInteract;

    public event UnityAction onPullGunTrigger;

    public event UnityAction onReleaseGunTrigger;
    void OnEnable()
    {
        playerControl = new PlayerControl();

        playerControl.GamePlay.SetCallbacks(this);
    }

    void OnDisable()
    {
        DisableAllInputs();
    }
    public void DisableAllInputs()
    {
        playerControl.GamePlay.Disable();
    }
    public void EnableGameplayInput()
    {
        playerControl.GamePlay.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if(onMove != null)
                onMove.Invoke(context.ReadValue<Vector2>());
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            if (onStopMove != null)
                onStopMove.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if (onJump != null)
                onJump.Invoke();
        }
    }


    public void OnStealth(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (onStealth != null)
                onStealth.Invoke();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (onDash != null)
                onDash.Invoke();
        }
    }

    public void OnPullGunTrigger(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (onPullGunTrigger != null)
                onPullGunTrigger.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            if (onReleaseGunTrigger != null)
                onReleaseGunTrigger.Invoke();
        }

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (onInteract != null)
                onInteract.Invoke();
        }
    }
}
