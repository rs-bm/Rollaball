using UnityEngine;

public class MirrorBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 normal = transform.forward;
            print(normal);
            Vector3 reflectedVelocity = Vector3.Reflect(rb.linearVelocity, normal);
            rb.linearVelocity = reflectedVelocity;
        }
    }
}
