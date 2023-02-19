using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrderInteraction : BaseInteraction
{
    [SerializeField] private InventoryItem appetizerItem;
    [SerializeField] private InventoryItem drinkItem;
    [SerializeField] private InventoryItem entreeItem;

    private Customer m_customer;

    protected override void Action()
    {
        Debug.Log("ACTION");
        IEnumerator<InventoryItem> items = InventoryManager.instance.GetItems().GetEnumerator();
        items.MoveNext();
        Debug.Log(items.Current);
        InventoryItem delivery = items.Current;
        m_customer.CurrentTable.Deliver(ItemToCustomerOrder(delivery));
        InventoryManager.instance.RemoveItem(delivery);
    }

    private void Start()
    {
        m_customer = GetComponent<Customer>();
    }

    private Customer.Order ItemToCustomerOrder (InventoryItem item)
    {
        if (item == appetizerItem)
        {
            return Customer.Order.Appetizer;
        }
        if (item == drinkItem)
        {
            return Customer.Order.Drink;
        }
        if (item == entreeItem)
        {
            return Customer.Order.Entree;
        }
        return Customer.Order.Entree;
    }
}
