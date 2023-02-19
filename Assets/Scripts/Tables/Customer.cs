using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public enum Order
    {
        Appetizer,
        Drink,
        Entree
    }

    public Table CurrentTable;
    public Order CurrentOrder;

    public void RandomizeOrders ()
    {
        CurrentOrder = (Order) Random.Range(0, 3);
    }

    public void FulfillRequest ()
    {

    }

    public override string ToString()
    {
        switch (CurrentOrder)
        {
            case Order.Appetizer:
                return "A";
            case Order.Drink:
                return "D";
            case Order.Entree:
                return "E";
            default:
                return "?";
        }
    }
}
