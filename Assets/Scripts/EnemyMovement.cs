using System;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
 // Reference to the player's transform.
 public Transform player;

 // Reference to the NavMeshAgent component for pathfinding.
 private NavMeshAgent navMeshAgent;


 // Start is called before the first frame update.
 void Start() {
      navMeshAgent = GetComponent<NavMeshAgent>();
    }

 // Update is called once per frame.
 void Update() {
 if (player != null && navMeshAgent.enabled)
        {    
            navMeshAgent.SetDestination(player.position);
        }
    }
}