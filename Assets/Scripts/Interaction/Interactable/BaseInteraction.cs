using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteraction : MonoBehaviour
{
    private void OnEnable()
    {
        ClickableObject.OnLeftClick += HandleInteraction;
    }

    private void OnDisable()
    {
        ClickableObject.OnLeftClick -= HandleInteraction;
    }

    public void HandleInteraction(GameObject origin, Vector3 position)
    {
        if (origin == this.gameObject)
        {
            Action();
            PostAction();
        }
    }

    public void HandleHoverEnter(GameObject origin, Vector3 position)
    {
        if (origin == this.gameObject)
        {
            HoverEnter();
        }
    }

    public void HandleHoverExit(GameObject origin, Vector3 position)
    {
        if (origin == this.gameObject)
        {
            HoverExit();
        }
    }

    protected virtual void PostAction() { }

    protected abstract void Action();

    protected virtual void HoverEnter() { }

    protected virtual void HoverExit() { }


}
