using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;   
    public Transform playerObj;     
    public float rotationSpeed = 7f;

    private void Update()
    {
        
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        orientation.forward = camForward.normalized;

       
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        
        if (inputDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDir.normalized);
            playerObj.rotation = Quaternion.Slerp(playerObj.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
