using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] MovementController controller;
    [SerializeField] CollisionInteractionDriver interactionDriver;

    public void Move (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.Move(context.ReadValue<float>());
        }
        else if (context.canceled)
        {
            controller.Stop();
        }
    }

    public void Interact (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactionDriver.Interact();
        }
    }
}
