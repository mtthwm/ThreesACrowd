using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCapacityPrerequisite : BasePrerequisite
{
    InventoryInteraction m_inventoryInteraction;

    private void Start()
    {
        m_inventoryInteraction = GetComponent<InventoryInteraction>();
    }

    public override bool Check()
    {
        return InventoryManager.instance.CheckCapacity(m_inventoryInteraction.GetItems().Length);
    }
}
