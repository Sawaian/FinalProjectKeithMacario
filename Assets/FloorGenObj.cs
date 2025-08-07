using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FloorGenObj : MonoBehaviour
{

    [SerializeField] TextMeshPro textCoordinates;
    private float xPosition;
    private float yPosition;

    void Start()
    {
        textCoordinates = GetComponent<TextMeshPro>();
    }

    void Update()
    {
         
        xPosition = transform.position.x;
        yPosition = transform.position.y;

        //textCoordinates.text = "X: " + xPosition + " Y: " + yPosition;
    }
}
