using UnityEngine;

public class Mono : MonoBehaviour
{
    void FixedUpdate()
    {
        rigidbody.MoveRotation(
            rigidbody.rotation * Quaternion.Euler(0f, 90f * Time.fixedDeltaTime, 0f)
        );
    }

    Rigidbody rigidbody;
    void Awake() => rigidbody = GetComponent<Rigidbody>();
}
