using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private float movementX;
    private float movementY;
    public float speed = 0; 
    private int WIN_COUNT = 8;
    public AudioSource pickupAudioSource;
    public AudioSource winAudioSource;
    public ParticleSystem winEffect;
    private Boolean trailPlaying = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent <Rigidbody>(); 
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 
        Vector3 velocity = -rb.linearVelocity;
        if (velocity.magnitude > 1)
        {
            if (trailPlaying == false)
            {
                GetComponent<ParticleSystem>().Play();
                trailPlaying = true;

            }
            GetComponent<ParticleSystem>().transform.rotation = Quaternion.LookRotation(velocity);
        }
        if (velocity.magnitude < 1)
        {
            GetComponent<ParticleSystem>().Stop();
            trailPlaying = false;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            pickupAudioSource.Play();
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text =  "Count: " + count.ToString();
        if (count >= WIN_COUNT)
        {
            winAudioSource.Play();
            winEffect.Play();
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = ":(";
        }
    }
}
