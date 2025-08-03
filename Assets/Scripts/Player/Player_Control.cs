using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Control : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 2.94f;
    [SerializeField] float rotationSpeed = 7.33f;

    [Header("References")]
    public Animator animator;
    public Transform playerObj;
    public Transform cameraTransform;
    public bool shouldFaceMovedirection = true;

    private Vector2 movementInput;
    private Rigidbody rigidBody;
    private bool isMoving;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if (animator != null)
            Debug.Log("Animator assigned");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log($"Movement input received: {movementInput}");
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = GetCameraRelativeDirection();
        HandleMovement(moveDirection);
    }

    Vector3 GetCameraRelativeDirection()
    {
        if (cameraTransform == null)
        {
            Debug.LogWarning("Camera transform not assigned.");
            return Vector3.zero;
        }

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        return (forward * movementInput.y + right * movementInput.x).normalized;
    }

    void HandleMovement(Vector3 moveDirection)
    {
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            Vector3 newPosition = rigidBody.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rigidBody.MovePosition(newPosition);
            isMoving = true;

            if (shouldFaceMovedirection)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                playerObj.rotation = Quaternion.Slerp(
                    playerObj.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                );
            }
        }
        else
        {
            isMoving = false;
        }

        if (animator != null)
            animator.SetFloat("Speed", isMoving ? 1f : 0f); 
    }
}
