using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class Inventory_Manager : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    public GameObject InventoryItem;
    public Transform ItemContent;
    public static Inventory_Manager instance;
    public List<Item> Items = new List<Item>();

    public Item item;

    private bool isActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        instance = this;
        inventory.SetActive(false);
        isActive = false;
    }
    void Update()
    {
        CloseInventory();
    }

    private void CloseInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && isActive)
        {
            isActive = false;
            inventory.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !isActive)
        {
            ListItems();
            isActive = true;
            inventory.SetActive(true);
            Debug.Log("Active was pressed");
        }
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }
    public void RemoveItem()
    {
        Items.Remove(item);
    }

    public void ListItems()
{
    if (InventoryItem == null) { Debug.LogError("InventoryItem is NULL"); return; }
    if (ItemContent   == null) { Debug.LogError("ItemContent is NULL");   return; }

    // Clear old rows
    for (int i = ItemContent.childCount - 1; i >= 0; i--)
        Destroy(ItemContent.GetChild(i).gameObject);

    foreach (var it in Items)
    {
        var row = Instantiate(InventoryItem, ItemContent);

        var nameTf = row.transform.Find("ItemName");
        if (nameTf == null)
        {
            Debug.LogError("Child 'ItemName' not found on InventoryItem prefab.");
            continue;
        }
        var nameTMP  = nameTf.GetComponent<TextMeshProUGUI>();
        var nameText = nameTf.GetComponent<UnityEngine.UI.Text>();

        if (nameTMP != null) nameTMP.text = it.itemName;
        else if (nameText != null) nameText.text = it.itemName;
        else Debug.LogError("'ItemName' has neither TextMeshProUGUI nor UnityEngine.UI.Text.");

        // --- ItemIcon (UGUI Image) ---
        var iconTf = row.transform.Find("ItemIcon");
        var iconImg = iconTf ? iconTf.GetComponent<UnityEngine.UI.Image>() : null;
        if (iconImg != null) iconImg.sprite = it.icon;
        else Debug.LogError("Child 'ItemIcon' missing or has no UnityEngine.UI.Image.");
    }
}
}
