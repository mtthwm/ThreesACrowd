using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    private BaseInteraction m_interaction;

    private void Start()
    {
        m_interaction = GetComponent<BaseInteraction>();
    }

    private bool CheckPrerequisites()
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

    public void TriggerInteraction()
    {
        m_interaction.HandleInteraction(gameObject, transform.position);
    }
}
