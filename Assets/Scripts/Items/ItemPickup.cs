using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item itemData; 
    private void PickUp()
    {
        Inventory_Manager.instance.AddItem(itemData);
        Destroy(gameObject); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
}
