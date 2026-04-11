using System;
using UnityEngine;

public class DimLampBehavior : MonoBehaviour
{
    public GameObject pointLight;
    public GameObject dimPointLight;
    public GameObject light;
    public GameObject dimLight;
    Boolean lit = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !lit)
        {
            print("player touched lamp");
            GetComponent<ParticleSystem>().Play();
            pointLight.SetActive(true);
            light.SetActive(true);
            dimPointLight.SetActive(false);
            dimLight.SetActive(false);
            lit = true;
        }
        if (other.gameObject.CompareTag("Enemy") && lit)
        {
                       print("enemy touched lamp");

                // add different particle effect
                GetComponent<ParticleSystem>().Play();
                pointLight.SetActive(false);
                light.SetActive(false);
                dimPointLight.SetActive(true);
                dimLight.SetActive(true);
                lit = false;
                // add sound effect
            
        }
    }
}
