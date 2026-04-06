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
            GetComponent<ParticleSystem>().Play();
            pointLight.SetActive(true);
            light.SetActive(true);
            dimPointLight.SetActive(false);
            dimLight.SetActive(false);
            lit = true;
        }
        
    }
}
