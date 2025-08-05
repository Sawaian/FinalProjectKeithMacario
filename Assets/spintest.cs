using UnityEngine;

public class SpinTest : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);
    }
}
