using System;
using UnityEngine;

public class UnlitLampBehavior : MonoBehaviour
{
    public GameObject pointLight;
    public GameObject light;
    Boolean lit = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            if (!lit && other.gameObject.GetComponent<Rigidbody>().linearVelocity.magnitude > 10) {
                GetComponent<ParticleSystem>().Play();
                pointLight.SetActive(true);
                light.SetActive(true);
                lit = true;
            }
        }
    }
}
