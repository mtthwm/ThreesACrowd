using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManagerTester : MonoBehaviour
{
    [SerializeField] TableManager tableManager;
    [SerializeField] Table table1;
    [SerializeField] Table table2;

    void Start()
    {
        Debug.Log(tableManager);
        table1.Deliver(Customer.Order.Drink);
        Debug.Log(tableManager);
        //table2.Deliver(Customer.Order.Drink);
        //Debug.Log(tableManager);
        //table1.Deliver(Customer.Order.Entree);
        //Debug.Log(tableManager);
        //table2.Deliver(Customer.Order.Appetizer);
        //Debug.Log(tableManager);
        //table2.Deliver(Customer.Order.Entree);
        //Debug.Log(tableManager);
    }
}
