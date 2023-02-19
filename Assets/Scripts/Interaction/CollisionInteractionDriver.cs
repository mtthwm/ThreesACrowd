using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionInteractionDriver : MonoBehaviour
{
    private CollidableObject m_obj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollidableObject obj = collision.gameObject.GetComponent<CollidableObject>();
        if (obj != null)
        {
            m_obj = obj;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CollidableObject obj = collision.gameObject.GetComponent<CollidableObject>();
        if (obj != null)
        {
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
