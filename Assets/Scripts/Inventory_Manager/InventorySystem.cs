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
    //Generic list that can hold inventory Slots.
    [SerializeField] private List<InventorySlot> inventorySlots;

    //We need to know how many slots are left.

    public List<InventorySlot> InventorySlots => inventorySlots;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    //We want the public variable to have the InventorySlot list count whenever we need it.
    public int InventorySize => InventorySlots.Count;

    //This will populate the inventory System with slot spaces. We will need to represent this
    //in canvas next. 
    public InventorySystem(int size)
    {
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }


}
