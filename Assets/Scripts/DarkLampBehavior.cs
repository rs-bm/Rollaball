using System;
using UnityEngine;

public class DarkLampBehavior : MonoBehaviour
{
    public GameObject pointLight;
    public GameObject light;
    public float speedReq = 11;
    Boolean lit = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !lit && other.gameObject.GetComponent<Rigidbody>().linearVelocity.magnitude > speedReq) {
            GetComponent<ParticleSystem>().Play();
            pointLight.SetActive(true);
            light.SetActive(true);
            lit = true;
            // REMINDER: add sound effect
            other.gameObject.GetComponent<PlayerController>().editCount(1);
        }
        if (other.gameObject.CompareTag("Enemy") && lit)
        {
            // REMINDER: add different particle effect
            GetComponent<ParticleSystem>().Play();
            pointLight.SetActive(false);
            light.SetActive(false);
            lit = false;
            // REMINDER: add sound effect
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().editCount(-1);
        }
    }
}
