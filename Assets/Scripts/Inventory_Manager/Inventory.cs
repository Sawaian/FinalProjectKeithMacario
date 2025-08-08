using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Events;
//Credit for learning these goes to Dan Pos, Youtuber.
//All of this is temporary. Will change it to meet the needs of the
//project. 

[System.Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem inventorySystem;

    public InventorySystem InventorySystem => inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequest;

    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize);
    }
}
