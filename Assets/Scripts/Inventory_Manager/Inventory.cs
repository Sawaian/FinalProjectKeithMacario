using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Events;
//Credit for learning these goes to Dan Pos, Youtuber.
//All of this is temporary. Will change it to meet the needs of the
//project. 

[System.Serializable]
//Will be a monobehavior because we are going to attach it to an object. 
public class Inventory : MonoBehaviour
{

    [SerializeField] int inventorySize = 5;

    private InventorySystem playerInventory;

    void Awake()
    {
        playerInventory = new InventorySystem(inventorySize); 
    }

}
