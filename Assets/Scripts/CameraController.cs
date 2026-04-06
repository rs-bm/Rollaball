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
        
        float hOffset = Mathf.Clamp(targetRigidbody.linearVelocity.x, -10, 10);
        float vOffset = Mathf.Clamp(targetRigidbody.linearVelocity.z, -10, 10);

        transform.rotation = Quaternion.Euler(new Vector3(-vOffset, 0, hOffset));

        // NEEDS TO BE SMOOTHED! and subtler!
        // Slamming into walls in this state is liable to give the player a concussion in real life
        // but u can see the vision...
    }
}
