using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPrerequisite : BasePrerequisite
{
    [SerializeField] private InventoryItem[] inventoryItems;
    [SerializeField] private bool remove;
    [SerializeField] private bool oneTime;

    private bool m_hasBeenSatisfied;

    public override bool Check()
    {
        if (oneTime && m_hasBeenSatisfied)
        {
            return true;
        }

        bool result = true;
        foreach (InventoryItem item in inventoryItems)
        {
            if (!InventoryManager.instance.CheckItem(item))
            {
                result = false;
                break;
            }
        }

        if (!result)
        {
            return false;
        }
        else
        {
            foreach (InventoryItem item in inventoryItems)
            {
                InventoryManager.instance.RemoveItem(item);
            }
            m_hasBeenSatisfied = true;
            return true;
        }
    }
}
