
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player_Control : MonoBehaviour
{
    Vector2 movement;
    public Animator animator;

    Rigidbody rigidBody;
    CapsuleCollider capsuleCollider;
    [SerializeField] float moveSpeed = 2.94f;
    [SerializeField] float rotationSpeed = 7.33f;
    public Transform playerObj;
    private Quaternion targetRotation;
    private bool isMoving;

    public Transform cameraTransform;

  void Awake()
{
    rigidBody = GetComponent<Rigidbody>();
    capsuleCollider = GetComponent<CapsuleCollider>();
    animator = GetComponentInChildren<Animator>();

    if (animator == null)
    {
        Debug.LogWarning("Animator not found!");
    }
    else
    {
        Debug.Log("Animator assigned");
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

  
    
public void HandleRotate(Vector3 moveDirection)
{
    if (isMoving && moveDirection.sqrMagnitude > 0.01f)
    {
        // Flatten direction and normalize
        Vector3 flatDir = new Vector3(moveDirection.x, 0, moveDirection.z).normalized;

        // Prevent flipping: only rotate when angle is not too extreme
        if (Vector3.Dot(playerObj.forward, flatDir) > -0.95f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatDir);
            playerObj.rotation = Quaternion.Slerp(
                playerObj.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }
}



    void HandleMovement()
    {
        //Previously I passed newPosition. That was an error. It must be moveDirection. Where
        //The character will go versus where it will be at upon completion. 

        Vector3 camForward = cameraTransform.forward;
            camForward.y = 0;
            camForward.Normalize();

            Vector3 camRight = cameraTransform.right;
            camRight.y = 0;
            camRight.Normalize();

            Vector3 moveDirection = camForward * movement.y + camRight * movement.x;

            if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();



        Vector3 currenPosition = rigidBody.position;

        //Vector 1 + Vector 2 * a scalar. 
        Vector3 newPosition = currenPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPosition);

        if (newPosition != currenPosition)
        {
            isMoving = true;
        }
        else {
            isMoving = false;
        }

        HandleRotate(moveDirection);


        float speed = moveDirection.magnitude;
        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        };

    }
    
      private bool IsGrounded()
    {
        float extraAtBottomCapsuleHeight = 0.15f; // Buffer distance below the collider to avoid missing the ground.
        float groundRadius = 0.3f; //Radius of the sphere used in the checks.

        RaycastHit raycastHit;

        //casts a sphere  downward from the center of the capsule to check for ground beneath;
        bool isHit = Physics.SphereCast(
            capsuleCollider.bounds.center, //Starting point : center of the collider
            groundRadius, //Radius of Sphere
            Vector3.down, //Direction of the cast (straight down)
            out raycastHit, //Output teh hit Info
            capsuleCollider.bounds.extents.y + extraAtBottomCapsuleHeight //Distance to check
            );

        Color rayColor = isHit ? Color.green : Color.red;

        //Visualize the the ray in the scene for debugging purposes.
        Debug.DrawRay(
            capsuleCollider.bounds.center,
            Vector3.down * (capsuleCollider.bounds.extents.y + extraAtBottomCapsuleHeight),
            rayColor,
            1f
        );
        return isHit;
    }

}
