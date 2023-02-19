using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryInteraction : BaseInteraction
{
    public delegate void InventoryHoverAction (InventoryItem[] items);
    public event InventoryHoverAction OnHover;

    private enum InventoryModificationMode
    {
        Add,
        Remove
    }

    [SerializeField] private InventoryModificationMode mode;
    [SerializeField] private InventoryItem[] items;

    protected override void Action()
    {
        if (mode == InventoryModificationMode.Add)
        {
            foreach (InventoryItem item in items)
            {
                InventoryManager.instance.AddItem(item);
            }
        }
        else
        {
            foreach (InventoryItem item in items)
            {
                InventoryManager.instance.RemoveItem(item);
            }
        }
    }

    protected override void Hover()
    {
        OnHover?.Invoke(items);
    }

    public InventoryItem[] GetItems ()
    {
        return items.ToArray();
    }
}
