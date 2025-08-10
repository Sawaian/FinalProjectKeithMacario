using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public Item item;
    void PickUp()
    {
        Inventory_Manager.instance.AddItem(item);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Player"))
    {
        PickUp();
    }
    }
}
