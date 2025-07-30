
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Control : MonoBehaviour
{
    Vector2 movement;
  public Animator animator;

    Rigidbody rigidBody;
    [SerializeField] float moveSpeed = 5f;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator not found!");
        }
        else
        {
            Debug.Log("Animator assigned automatically.");

        }
    }


    void FixedUpdate()
    {
        HandleMovement();
        
        if (animator == null)
{
    Debug.LogWarning("Animator is NOT assigned!");
}
else
{
    Debug.Log("Animator IS assigned.");
}
 
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        Debug.Log(movement);
    }

    void HandleMovement()
    {
        Vector3 currenPosition = rigidBody.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        //Vector 1 + Vector 2 * a scalar. 
        Vector3 newPosition = currenPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPosition);
        Debug.Log("Triggered");

        float speed = moveDirection.magnitude;
        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }
    
    }
}
