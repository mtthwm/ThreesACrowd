using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerReact : MonoBehaviour
{
    private Customer m_customer;
    private Animator m_animator;

    private void Start()
    {
        m_customer = GetComponent<Customer>();
        m_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        InventoryInteraction.OnHoverEnter += Hover;
        InventoryInteraction.OnHoverExit += Exit;
    }

    private void OnDisable()
    {
        InventoryInteraction.OnHoverEnter -= Hover;
        InventoryInteraction.OnHoverExit -= Exit;
    }

    private void Hover (InventoryItem[] items)
    {
        Customer.Order order = m_customer.CurrentTable.ItemToCustomerOrder(items[0]);
        if (order == m_customer.CurrentOrder)
        {
            m_animator.SetBool("Happy", true);
        } else
        {
            m_animator.SetBool("Sad", true);
        }
    }

    private void Exit(InventoryItem[] items)
    {
        m_animator.SetBool("Happy", false);
        m_animator.SetBool("Sad", false);
    }
}
