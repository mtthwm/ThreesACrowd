using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public delegate void InventoryAction ();
    public static event InventoryAction OnModifyInventory;

    public static InventoryManager instance;

    private List<InventoryItem> items = new();

    [SerializeField] private int maxCapacity;

    void Awake()
    {
        // Singleton Pattern
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public bool AddItem (InventoryItem item)
    {
        if (items.Count <= maxCapacity)
        {
            items.Add(item);
            OnModifyInventory?.Invoke();
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool CheckItem (InventoryItem item)
    {
        return items.Contains(item);
    }

    public bool RemoveItem(InventoryItem item)
    {
        bool result = items.Remove(item);
        OnModifyInventory?.Invoke();
        return result;
    }

    public IEnumerable<InventoryItem> GetItems ()
    {
        return items.ToArray();
    }

    public bool CheckCapacity (int countToAdd)
    {
        return items.Count + countToAdd <= maxCapacity;
    }
}
