using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    [Tooltip("If this is not null, this component will only send messages when this object is within a certain range")]
    [SerializeField] private Transform requireProximityObject;
    [SerializeField] private float requireProximityRadius;

    public delegate void MouseAction(GameObject origin, Vector3 position);
    public static event MouseAction OnRightClick;
    public static event MouseAction OnLeftClick;
    public static event MouseAction OnHoverEnter;
    public static event MouseAction OnHoverExit;

    private bool m_mouse_hover = false;
    private bool m_left_clicked = false;
    private bool m_right_clicked = false;

    private void Update()
    {
        HandleHover();
    }

    private void HandleHover()
    {
        if (m_mouse_hover)
        {
            if (Input.GetMouseButton(0))
            {
                if (!m_left_clicked && CheckPrerequisites())
                {
                    m_left_clicked = true;
                    OnLeftClick?.Invoke(this.gameObject, MousePosition());
                }
            }
            else
            {
                m_left_clicked = false;
            }
            if (Input.GetMouseButton(1))
            {
                if (!m_right_clicked && CheckPrerequisites())
                {
                    OnRightClick?.Invoke(this.gameObject, MousePosition());
                    m_right_clicked = true;
                }
            }
            else
            {
                m_right_clicked = false;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (CheckProximity())
        {
            OnHoverEnter?.Invoke(this.gameObject, MousePosition());
            m_mouse_hover = true;
        }
    }

    private void OnMouseExit()
    {
        if (CheckProximity())
        {
            OnHoverExit?.Invoke(this.gameObject, MousePosition());
            m_mouse_hover = false;
        }
    }

    private bool CheckProximity ()
    {
        if (requireProximityObject == null)
        {
            return true;
        }

        float dist = Vector3.Distance(transform.position, requireProximityObject.position);
        return dist < requireProximityRadius;
    }

    private bool CheckPrerequisites ()
    {
        BasePrerequisite[] prerequisites = GetComponents<BasePrerequisite>();

        foreach (BasePrerequisite prerequisite in prerequisites)
        {
            if (!prerequisite.Check())
            {
                return false;
            }
        }

        return true;
    }

    public static Vector3 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
