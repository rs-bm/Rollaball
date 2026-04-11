using System;
using JetBrains.Annotations;
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
            other.gameObject.GetComponent<PlayerController>().editCount(1);
        }
        if (other.gameObject.CompareTag("Enemy") && lit)
        {
            // REMINDER: add different particle effect
            GetComponent<ParticleSystem>().Play();
            pointLight.SetActive(false);
            light.SetActive(false);
            dimPointLight.SetActive(true);
            dimLight.SetActive(true);
            lit = false;
            // REMINDER: add sound effect
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().editCount(-1);
        }
    }
}
