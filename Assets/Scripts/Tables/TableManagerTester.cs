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
        StartCoroutine(Test());
    }

    private IEnumerator Test ()
    {
        Debug.Log(tableManager);
        yield return new WaitForSeconds(5f);
        table1.Deliver(Customer.Order.Drink);
        Debug.Log(tableManager);
        yield return new WaitForSeconds(5f);
        table2.Deliver(Customer.Order.Appetizer);
        Debug.Log(tableManager);
    }
}
