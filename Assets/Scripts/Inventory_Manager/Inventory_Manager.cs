using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory_Manager : MonoBehaviour
{
    [SerializeField] GameObject inventory;

    private bool isActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
            isActive = true;
            inventory.SetActive(true);
            Debug.Log("Active was pressed");
        }
    }
}
