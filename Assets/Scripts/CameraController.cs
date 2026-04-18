using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public Transform targetTransform;
    public Rigidbody targetRigidbody;
    private Vector3 velocity = Vector3.zero;
    private float followSpeed = 5f;
    void LateUpdate()
    {
        if (targetTransform == null || targetRigidbody == null) {
            return;
        }
        
        float hOffset = Mathf.Clamp(targetRigidbody.linearVelocity.x, -5, 5);
        float vOffset = Mathf.Clamp(targetRigidbody.linearVelocity.z, -5, 5);

        Quaternion targetRotation = Quaternion.Euler(-vOffset * 0.5f, 0, hOffset * 0.5f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        // transform.position = Vector3.SmoothDamp(transform.position, targetTransform.position, ref velocity, 0.1f);
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * followSpeed);
    }
}
