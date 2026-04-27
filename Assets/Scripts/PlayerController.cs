using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    public float velociyMagnitude;
    private Rigidbody rb; 
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private float movementX;
    private float movementY;
    public float speed = 0; 
    private int WIN_COUNT = 2;
    public AudioSource pickupAudioSource;
    public AudioSource winAudioSource;
    public ParticleSystem winEffect;
    private Boolean trailPlaying = false;
    public GameObject rockWall;
    public GameObject sun;


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
        if (velocity.magnitude > 3)
        {
            if (trailPlaying == false)
            {
                GetComponent<ParticleSystem>().Play();
                trailPlaying = true;

            }
            GetComponent<ParticleSystem>().transform.rotation = Quaternion.LookRotation(velocity);
        }
        if (velocity.magnitude < 3)
        {
            GetComponent<ParticleSystem>().Stop();
            trailPlaying = false;
        }
        velociyMagnitude = velocity.magnitude;
    }

    public void editCount(int change)
    {
        count += change;
        SetCountText();
    }

    void SetCountText()
    {
        countText.text =  "Count: " + count.ToString();
        if (count >= WIN_COUNT) {
            onWin();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "game over :(";
            collision.gameObject.GetComponentInChildren<Animator>().SetFloat("speed_f", 0);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LevelTwo")) {
            sun.gameObject.GetComponent<Animator>().SetTrigger("Sunset");
        }
    }
    private void onWin()
    {
        winAudioSource.Play();
        winEffect.Play();
        winTextObject.SetActive(true);
        rockWall.GetComponent<Animator>().SetTrigger("Clear");
        GameObject[] toDestroy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in toDestroy) {
            Destroy(obj);
        }
        toDestroy = null;
    }
}
