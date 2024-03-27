using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 originalPos = new Vector3(-1739, 5, 835);
    public PlayerMovement playerMovement;
    public Transform respawnPoint;

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("water"))
        {
            Debug.Log("Player is in water");
            //playerMovement.enabled = false;
            transform.position = respawnPoint.position;
            //playerMovement.enabled = true;
        }
    }
}
