using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory_Manager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject inventoryPanel;     
    [SerializeField] private GameObject inventoryRowPrefab; 
    [SerializeField] private Transform itemContent;         

    public static Inventory_Manager instance;

    private bool isActive = false;

    public List<Item> items = new List<Item>();

    void Awake()
    {
        instance = this;
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = !isActive;
            inventoryPanel.SetActive(isActive);

            if (isActive)
                ListItems(); 
        }
    }

    public void AddItem(Item newItem)
    {
        items.Add(newItem);


        if (isActive) 
            ListItems();
    }

    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
        if (isActive) 
            ListItems();
    }

    private void ListItems()
    {
        if (inventoryRowPrefab == null)
        {
            Debug.LogError("UI Row Prefab not assigned!");
            return;
        }

      
        for (int i = itemContent.childCount - 1; i >= 0; i--)
            Destroy(itemContent.GetChild(i).gameObject);


        foreach (var it in items)
        {
            GameObject row = Instantiate(inventoryRowPrefab, itemContent);

            var nameText = row.transform.Find("ItemName")?.GetComponent<TextMeshProUGUI>();
            var iconImage = row.transform.Find("ItemIcon")?.GetComponent<Image>();

            if (nameText != null) nameText.text = it.ItemName;
            if (iconImage != null) iconImage.sprite = it.ItemIcon;
            
if (iconImage != null)
{
    iconImage.color = Color.white;      // kill any tint
    iconImage.sprite = it.ItemIcon;     // from your Item SO
    iconImage.enabled = iconImage.sprite != null;
}

        }
    }
}
