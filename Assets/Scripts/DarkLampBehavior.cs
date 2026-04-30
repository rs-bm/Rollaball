using System;
using UnityEngine;

public class DarkLampBehavior : MonoBehaviour
{
    public GameObject pointLight;
    public GameObject light;
    public float speedReq = 11;
    public Boolean lit = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !lit && other.gameObject.GetComponent<Rigidbody>().linearVelocity.magnitude > speedReq) {
            GetComponent<ParticleSystem>().Play();
            other.gameObject.GetComponent<PlayerController>().setCount(other.gameObject.GetComponent<PlayerController>().getCount() + 1);
            activate();
        }
        if (other.gameObject.CompareTag("Enemy") && lit && GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {
            GetComponent<ParticleSystem>().Play();
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().setCount(GameObject.FindWithTag("Player").GetComponent<PlayerController>().getCount() - 1);
            deactivate();
        }
    }
    public void activate() {
        pointLight.SetActive(true);
        light.SetActive(true);
        lit = true;
    }
    public void deactivate() {
        pointLight.SetActive(false);
        light.SetActive(false);
        lit = false;
    }
}
