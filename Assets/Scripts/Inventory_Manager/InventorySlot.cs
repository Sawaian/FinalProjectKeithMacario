using Unity.VisualScripting;
using UnityEngine;
//Credit for learning these goes to Dan Pos, Youtuber. 
//All of this is temporary. Will change it to meet the needs of the
//project. 

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private Item itemData;
    [SerializeField] private int stackSize;

    public Item Data => itemData;

    public int StackSize => stackSize;

    public InventorySlot(Item source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        itemData = null;
        stackSize = -1;
    }
}
