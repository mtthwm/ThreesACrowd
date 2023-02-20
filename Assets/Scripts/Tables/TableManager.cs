using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public delegate void TableClearAction();
    public static event TableClearAction OnClearTable;

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
    }

    public void RemovePairs ()
    {
        foreach (Table t in tables)
        {
            bool removed = t.RemovePair();
            if (removed)
            {
                OnClearTable?.Invoke();
            }
        }

        if (CheckClear())
        {
            GenerateCustomers();
        }
    }

    public bool CheckClear()
    {
        int count = 0;
        foreach (Table t in tables)
        {
            count += t.maxSize - t.Vacancies;
        }
        return count == 1;
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
        foreach (Table t in tables)
        {
            t.Clear();
        }

        int count = tables.Count * 2 + 1;

        Customer.Order?[] orders = new Customer.Order?[count];
        int filledCount = 0;
        for (int i = 0; i < count / 2; i++)
        {
            Customer.Order order = (Customer.Order) Random.Range(0, 3);

            for (int j = 0; j < 2; j++)
            {
                int candidate = -1;
                while (candidate == -1 || orders[candidate] != null)
                {
                    candidate = Random.Range(0, orders.Length);
                }
                orders[candidate] = order;
                filledCount++;
            }
        }
        if (filledCount != orders.Length)
        {
            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i] == null)
                {
                    orders[i] = (Customer.Order)Random.Range(0, 3);
                }
            }
        }

        int tableIndex = 0;
        for (int i = 0; i < orders.Length; i++)
        {
            GameObject customerObject = Instantiate(customerPrefabs[m_currentPrefabIndex]);
            m_currentPrefabIndex = (m_currentPrefabIndex + 1) % customerPrefabs.Length;
            Customer c = customerObject.GetComponent<Customer>();
            c.CurrentOrder = (Customer.Order) orders[i];
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
