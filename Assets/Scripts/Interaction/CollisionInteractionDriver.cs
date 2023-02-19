using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CollisionInteractionDriver : MonoBehaviour
{
    [SerializeField] UnityEvent onHover;

    private CollidableObject m_obj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollidableObject obj = collision.gameObject.GetComponent<CollidableObject>();
        if (obj != null)
        {
            m_obj = obj;
            m_obj.TriggerHoverEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CollidableObject obj = collision.gameObject.GetComponent<CollidableObject>();
        if (obj != null)
        {
            m_obj.TriggerHoverExit();
            m_obj = null;
        }
    }

    public void Interact ()
    {
        if (m_obj != null)
        {
            m_obj.TriggerInteraction();
        }
    }
}
