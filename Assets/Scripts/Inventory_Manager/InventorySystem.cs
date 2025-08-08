using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
//Credit for learning these goes to Dan Pos, Youtuber. 
//All of this is temporary. Will change it to meet the needs of the
//project. 


[System.Serializable]
public class InventorySystem
{
    //List of generics under the InventorySlot type.
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

}
