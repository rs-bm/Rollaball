using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public Transform targetTransform;
    public Rigidbody targetRigidbody;
    void LateUpdate()
    {
        if (targetTransform == null || targetRigidbody == null) {
            return;
        }
        transform.position = targetTransform.position;
        
        float hOffset = Mathf.Clamp(targetRigidbody.linearVelocity.x, -7, 7);
        float vOffset = Mathf.Clamp(targetRigidbody.linearVelocity.z, -7, 7);

        Quaternion targetRotation = Quaternion.Euler(-vOffset, 0, hOffset);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }
}
