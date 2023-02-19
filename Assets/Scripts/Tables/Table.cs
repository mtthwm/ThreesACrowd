using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Customer;

public class Table : MonoBehaviour
{
    [Header("Display stuff")]
    [SerializeField] private float spacing = 1.5f;

    [Header("API Stuff")]
    [SerializeField] private int maxSize;
    [SerializeField] public Customer[] occupants;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Translation")]
    [SerializeField] private InventoryItem appetizerItem;
    [SerializeField] private InventoryItem drinkItem;
    [SerializeField] private InventoryItem entreeItem;

    public int TimerMax { get; set; } = 60000;
    public int Timer { get; set; } = 60000;

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

    private void Update()
    {
        if (maxSize - Vacancies == 3)
        {
            Timer -= (int)(Time.deltaTime * 1000);
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
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
    public Order ItemToCustomerOrder(InventoryItem item)
    {
        if (item == appetizerItem)
        {
            return Order.Appetizer;
        }
        if (item == drinkItem)
        {
            return Order.Drink;
        }
        if (item == entreeItem)
        {
            return Order.Entree;
        }
        return Order.Entree;
    }

    public InventoryItem OrderToCustomerItem(Order order)
    {
        if (order == Order.Appetizer)
        {
            return appetizerItem;
        }
        if (order == Order.Drink)
        {
            return drinkItem;
        }
        if (order == Order.Entree)
        {
            return entreeItem;
        }
        return entreeItem;
    }
}
