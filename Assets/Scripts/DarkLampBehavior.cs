using System;
using UnityEngine;

public class DarkLampBehavior : MonoBehaviour
{
    public GameObject pointLight;
    public GameObject light;
    Boolean lit = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !lit && other.gameObject.GetComponent<Rigidbody>().linearVelocity.magnitude > 10) {
            
                GetComponent<ParticleSystem>().Play();
                pointLight.SetActive(true);
                light.SetActive(true);
                lit = true;
                // add sound effect
            
        }
        if (other.gameObject.CompareTag("Enemy") && lit)
        {
            print("something happened");
                // add different particle effect
                GetComponent<ParticleSystem>().Play();
                pointLight.SetActive(false);
                light.SetActive(false);
                lit = false;
                // add sound effect
            
        }
    }
}
