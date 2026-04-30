using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private int count;
    private int WIN_COUNT = 6;
    private float movementX;
    private float movementY;
    public float velociyMagnitude;
    public float speed = 0; 
    private Boolean trailPlaying = false;
    private Rigidbody rb; 
    public TextMeshProUGUI countText;
    
    public AudioSource pickupAudioSource;
    public AudioSource winAudioSource;
    private enum Level{One, Two};
    public Transform startPos;
    public Transform enemyStartPos;
    private Level level = Level.One;
    public GameObject enemy;
    public GameObject winTextObject;
    public GameObject loseUI;
    public GameObject levelOneLamps;
    public GameObject levelTwoLamps;
    public GameObject rockWall;
    public GameObject sun;
    

    void Start() {
        rb = GetComponent <Rigidbody>(); 
        setCount(0);
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
        if (velocity.magnitude > 4) {
            if (trailPlaying == false) {
                GetComponent<ParticleSystem>().Play();
                trailPlaying = true;
            }
            GetComponent<ParticleSystem>().transform.rotation = Quaternion.LookRotation(velocity);
        }
        if (velocity.magnitude < 3) {
            GetComponent<ParticleSystem>().Stop();
            trailPlaying = false;
        }
        velociyMagnitude = velocity.magnitude;
    }
    public int getCount() {
        return count;
    }

    public void setCount(int newCount) {
        count = newCount;
        countText.text =  "" + count.ToString() + " / " + WIN_COUNT;
        if (count >= WIN_COUNT) {
            onWin();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            print("??");
            onLose();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LevelTwo")) {
            sun.gameObject.GetComponent<Animator>().SetTrigger("Sunset");
            WIN_COUNT = 5;
            setCount(0);
        }
        if (other.gameObject.CompareTag("Fog")) {
            print("??!!");
            onLose();
        }
    }
    public void onWin() {
        winAudioSource.Play();
        if (level == Level.One) {
            rockWall.GetComponent<Animator>().SetTrigger("Clear");
            foreach (Transform child in enemy.transform) {
                Destroy(child.gameObject);
            }
            Destroy(enemy);
        } else if (level == Level.Two) {
            winTextObject.SetActive(true);
        }
    }

    public void onLose() {
        gameObject.SetActive(false);
        loseUI.SetActive(true);
        enemy.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void resetLevel() {
        loseUI.SetActive(false);
        // reset player position and activated lamp count
        transform.position = startPos.position;
        rb.linearVelocity = Vector3.zero;
        setCount(0);
        gameObject.SetActive(true);
        if (level == Level.One) {
            // reset level one lamps
            foreach(Transform child in levelOneLamps.transform) {
                if(child.gameObject.GetComponent<DimLampBehavior>() != null) {
                    child.gameObject.GetComponent<DimLampBehavior>().deactivate();
                }
                if (child.gameObject.GetComponent<DarkLampBehavior>() != null) {
                    child.gameObject.GetComponent<DarkLampBehavior>().deactivate();
                }
            }
            // reset enemy
            enemy.transform.position = enemyStartPos.position;
            enemy.GetComponent<NavMeshAgent>().enabled = true;
        } else if (level == Level.Two) {
            // reset level two lamps
            foreach(Transform child in levelTwoLamps.transform) {
                if(child.gameObject.GetComponent<DimLampBehavior>() != null) {
                    child.gameObject.GetComponent<DimLampBehavior>().deactivate();
                }
                if (child.gameObject.GetComponent<DarkLampBehavior>() != null) {
                    child.gameObject.GetComponent<DarkLampBehavior>().deactivate();
                }
            }
        }
        
    }
}
