using UnityEngine;
using System.Collections.Generic;

public class ItemQuickSwap : MonoBehaviour
{
    [SerializeField] private string item1 = "gun";
    [SerializeField] private string item2 = "axe";
     EnemyHealth enemyHealth = new EnemyHealth();


    
    private Stack<string> itemStack;

    void Start()
    {
        itemStack = new Stack<string>();
        itemStack.Push(item1);
        itemStack.Push(item2);
       
    }

    public void Attack()
    {

        enemyHealth.EnemyHit();
        // if (itemStack.Count == 2)
        // {
        //     string tempItem1 = itemStack.Pop();
        //     string tempItem2 = itemStack.Pop();

        //     itemStack.Push(tempItem2);
        //     itemStack.Push(tempItem1);


        //     string[] stackArray = itemStack.ToArray();
        //     Debug.Log("Items swapped. Current stack: " + stackArray[1] + " First: " + stackArray[0] + " Second: ");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }
}
