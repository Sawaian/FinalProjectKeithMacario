using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Hashtable enemyHealth = new Hashtable();

    void Start()
    {

        enemyHealth["Shambler"] = 100;
        enemyHealth["Ghoul"] = 150;
        enemyHealth["Wendigo"] = 250;
    }

    public void EnemyHit()
    {
        Debug.Log("Ghoul HP: " + enemyHealth["Ghoul"]);


        enemyHealth["Ghoul"] = (int)enemyHealth["Ghoul"] - 20;
        Debug.Log("Ghoul took damage, new HP: " + enemyHealth["Ghoul"]);
    }
}

