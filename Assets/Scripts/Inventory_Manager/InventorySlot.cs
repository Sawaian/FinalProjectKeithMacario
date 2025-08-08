using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class InventorySlot
{

    [SerializeField] Item itemData;

    public Item Data => itemData;
    
    
}
