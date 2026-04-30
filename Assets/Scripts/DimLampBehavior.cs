using System;
using JetBrains.Annotations;
using UnityEngine;

public class DimLampBehavior : MonoBehaviour
{
    public GameObject pointLight;
    public GameObject dimPointLight;
    public GameObject light;
    public GameObject dimLight;
    public GameObject player;

    Boolean lit = false;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !lit) {
            GetComponent<ParticleSystem>().Play();
            other.gameObject.GetComponent<PlayerController>().setCount(other.gameObject.GetComponent<PlayerController>().getCount() + 1);
            activate();
        }
        if (other.gameObject.CompareTag("Enemy") && lit && GameObject.FindGameObjectsWithTag("Player").Length == 1) {
            GetComponent<ParticleSystem>().Play();
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().setCount(GameObject.FindWithTag("Player").GetComponent<PlayerController>().getCount() - 1);
            deactivate();
        }
    }
    public void activate() {
        pointLight.SetActive(true);
            light.SetActive(true);
            dimPointLight.SetActive(false);
            dimLight.SetActive(false);
            lit = true;
    }
    public void deactivate() {
        pointLight.SetActive(false);
        light.SetActive(false);
        dimPointLight.SetActive(true);
        dimLight.SetActive(true);
        lit = false;
    }
}
