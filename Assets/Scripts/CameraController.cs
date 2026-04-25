using System;
using NUnit.Framework;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public Transform titleTransform;
    public Transform levelTransform;
    public Transform targetTransform;
    public Rigidbody targetRigidbody;
    private Vector3 velocity = Vector3.zero;
    private float followSpeed = 5f;
    private Boolean started = false;
    void Start()
    {
        camera.transform.position = titleTransform.position;
        camera.transform.rotation = titleTransform.rotation;
        camera.GetComponent<Camera>().fieldOfView = 45;
    }
    public void CameraSetUp()
    {
        started = true;
        camera.transform.position = levelTransform.position;
        camera.transform.rotation = levelTransform.rotation;
        camera.GetComponent<Camera>().fieldOfView = 30;

    }
    void LateUpdate()
    {
        if (started == false || targetTransform == null || targetRigidbody == null) {
            return;
        }
        
        float hOffset = Mathf.Clamp(targetRigidbody.linearVelocity.x, -7, 7);
        float vOffset = Mathf.Clamp(targetRigidbody.linearVelocity.z, -7, 7);

        Quaternion targetRotation = Quaternion.Euler(-vOffset * 0.2f, 0, hOffset * 0.2f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        // transform.position = Vector3.SmoothDamp(transform.position, targetTransform.position, ref velocity, 0.1f);
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * followSpeed);
    }
}
