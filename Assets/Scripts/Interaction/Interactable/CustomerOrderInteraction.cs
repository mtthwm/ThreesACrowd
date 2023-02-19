using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrderInteraction : BaseInteraction
{
    private Table m_table;

    protected override void Action()
    {
        IEnumerator<InventoryItem> items = InventoryManager.instance.GetItems().GetEnumerator();
        items.MoveNext();
        InventoryItem delivery = items.Current;
        m_table.Deliver(m_table.ItemToCustomerOrder(delivery));
        InventoryManager.instance.RemoveItem(delivery);
    }

    private void Start()
    {
        m_table = GetComponent<Table>();
    }
}
