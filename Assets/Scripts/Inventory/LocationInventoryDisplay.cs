using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationInventoryDisplay : MonoBehaviour
{
    [SerializeField] private Transform[] locations;
    [SerializeField] private Transform container;

    private void OnEnable()
    {
        InventoryManager.OnModifyInventory += UpdateDisplay;
    }

    private void OnDisable()
    {
        InventoryManager.OnModifyInventory -= UpdateDisplay;
    }

    private void UpdateDisplay ()
    {
        ClearContainer();
        InventoryItem[] items = InventoryManager.instance.GetItems().ToArray();
        for (int i = 0; i < items.Length; i++)
        {
            GenerateSpriteObject(items[i], locations[i]);
            i++;
        }
    }

    private GameObject GenerateSpriteObject (InventoryItem item, Transform t)
    {
        GameObject obj = new GameObject(item.id);
        SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
        spr.sprite = item.icon;
        obj.transform.parent = container;

        obj.transform.position = t.position;
        obj.transform.rotation = t.rotation;
        obj.transform.localScale = t.localScale;

        return obj;
    }

    private void ClearContainer ()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
