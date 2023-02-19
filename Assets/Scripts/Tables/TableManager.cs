using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{

    [SerializeField] private List<Table> tables = new List<Table>();

    [SerializeField] private GameObject[] customerPrefabs;

    private int m_currentPrefabIndex;

    private void Start()
    {
        GenerateCustomers();
        UpdatePositions();
    }

    public void Relocate (Customer c)
    {
        List<Table> otherTables = tables.FindAll((t) => t != c.CurrentTable);

        Table mostVacancies = null;
        foreach (Table t in otherTables)
        {
            if (mostVacancies == null)
            {
                if (t.Vacancies > 0)
                {
                    mostVacancies = t;
                }
                continue;
            }

            if (t.Vacancies > mostVacancies.Vacancies)
            {
                mostVacancies = t;
            }
        }

        if (mostVacancies != null)
        {
            c.CurrentTable.Remove(c);
            bool filledSpot = mostVacancies.FillVacancy(c);
        }

        RemovePairs();
    }

    private void RemovePairs ()
    {
        foreach (Table t in tables)
        {
            t.RemovePair();
        }
    }

    public void UpdatePositions ()
    {
        foreach (Table t in tables)
        {
            foreach (Customer c in t.occupants)
            {
                if (c != null)
                {
                    t.GetRealPosition(c);
                }
            }
        }
    }

    public override string ToString()
    {
        string buildString = "";

        foreach (Table t in tables)
        {
            buildString += " |";
            buildString += t.ToString();
            buildString += "| ";
        }

        return buildString;
    }

    public void GenerateCustomers ()
    {
        int count = 0;
        foreach (Table t in tables)
        {
            t.Clear();
            count += t.maxSize;
        }
        count -= 1;

        Customer.Order[] orders = new Customer.Order[count];
        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                orders[i] = (Customer.Order) Random.Range(0, 3);
            }
            // Yeah not my proudest moment
            orders[i] = (Customer.Order) (((int)orders[i - 1] + Random.Range(1, 3)) % 3);
        }

        int tableIndex = 0;
        for (int i = 0; i < orders.Length; i++)
        {
            GameObject customerObject = Instantiate(customerPrefabs[m_currentPrefabIndex]);
            m_currentPrefabIndex = (m_currentPrefabIndex + 1) % customerPrefabs.Length;
            Customer c = customerObject.GetComponent<Customer>();
            c.CurrentOrder = orders[i];
            c.CurrentTable = tables[tableIndex];
            tables[tableIndex].FillVacancy(c);

            // Move to next table
            if (tables[tableIndex].Vacancies == 0)
            {
                tableIndex++;
            }
        }
    }
}
