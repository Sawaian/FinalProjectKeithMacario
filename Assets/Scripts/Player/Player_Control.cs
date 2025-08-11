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

        if (cameraTransform == null)
        cameraTransform = GameObject.Find("Main Gameplay Camera").transform;

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
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * movementInput.y + right * movementInput.x;
        Vector3 newPosition = rigidBody.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rigidBody.MovePosition(newPosition);

        if (animator != null)
        {
            animator.SetFloat("Speed", moveDirection.magnitude);
        }


        // Rotation
        if (shouldFaceMovedirection && moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

        }

         
}
    }

    


    // Animator
   

