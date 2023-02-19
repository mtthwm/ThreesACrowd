using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [Header("Display stuff")]
    [SerializeField] private float spacing = 1.5f;

    [Header("API Stuff")]
    [SerializeField] private int maxSize;
    [SerializeField] public Customer[] occupants;

    public TableManager TableManager;
    public int Vacancies { 
        get {
            int count = 0;
            for (int i = 0; i < occupants.Length; i++)
            {
                if (occupants[i] != null)
                {
                    count++;
                }
            }
            return maxSize - count;
        }
    }

    public void Remove (Customer c)
    {
        for (int i = 0; i < occupants.Length; i++)
        {
            if (occupants[i] == c)
            {
                occupants[i] = null;
            }
        }
    }

    public int IndexOf (Customer c)
    {
        for (int i = 0; i < occupants.Length; i++)
        {
            if (occupants[i] == c)
            {
                return i;
            }
        }
        return -1;
    }

    private int NextAvailableIndex ()
    {
        for (int i = 0; i < occupants.Length; i++)
        {
            if (occupants[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    public bool FillVacancy (Customer c)
    {
        c.CurrentTable = this;
        int index = NextAvailableIndex();
        if (index == -1 || occupants[index] != null)
        {
            return false;
        }
        occupants[index] = c;
        return true;
    }

    public void RemovePair ()
    {
        if (Vacancies != maxSize - 2)
        {
            return;
        }

        Customer currentCustomer = null;
        for (int i = 0; i < occupants.Length; i++)
        {
            if (occupants[i] == null)
            {
                continue;
            }

            if (currentCustomer == null)
            {
                currentCustomer = occupants[i];
                continue;
            }

            if (currentCustomer.CurrentOrder != occupants[i].CurrentOrder)
            {
                return;
            }
        }

        for (int i = 0; i < occupants.Length; i++)
        {
            if (occupants[i] == null)
            {
                continue;
            }

            Destroy(occupants[i].gameObject);
            occupants[i] = null;
        }
    }

    public void Deliver (Customer.Order order)
    {
        foreach (Customer c in occupants)
        {
            if (c == null)
            {
                continue;
            }

            if (c.CurrentOrder != order)
            {
                TableManager.Relocate(c);
                c.FulfillRequest();
            }
        }

        TableManager.UpdatePositions();
    }

    public Vector2 GetRealPosition (Customer c)
    {
        int mid = maxSize / 2;
        int i = IndexOf(c) - mid;
        Debug.Log(i + " " + c.transform.name);
        Vector2 position = (Vector2)transform.position + Vector2.right * i * spacing;
        c.transform.position = position;
        return position;
    }

    public override string ToString()
    {
        string buildString = "";
        foreach (Customer c in occupants)
        {
            if (c == null)
            {
                buildString += "_";
                continue;
            }
            buildString += c.ToString();
        }
        buildString += ":";
        buildString += maxSize - Vacancies;
        return buildString;
    }
}
