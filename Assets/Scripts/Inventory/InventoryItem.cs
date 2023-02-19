using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/InventoryItem", order = 1)]
public class InventoryItem : ScriptableObject
{
    public string id;
    public Sprite icon;

    public override bool Equals(object other)
    {
        if (other is not InventoryItem)
        {
            return false;
        }

        return id.Equals(((InventoryItem)other).id);
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }

    public static bool operator == (InventoryItem item1, InventoryItem item2)
    {
        return item1.Equals(item2);
    }

    public static bool operator !=(InventoryItem item1, InventoryItem item2)
    {
        return !item1.Equals(item2);
    }
}
