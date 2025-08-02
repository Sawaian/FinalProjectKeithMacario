using UnityEngine;

public class CamFollowTarget : MonoBehaviour
{
    public Transform playerObj;             
    public Transform cameraFollowTarget;

    void LateUpdate()
    {
        if (cameraFollowTarget != null && playerObj != null)
        {
            cameraFollowTarget.rotation = Quaternion.Euler(0, playerObj.eulerAngles.y, 0);
        }
    }
}
