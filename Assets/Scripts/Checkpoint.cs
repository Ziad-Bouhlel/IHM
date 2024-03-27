using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // needed to use UnityEvent

public class Checkpoint : MonoBehaviour
{
    public UnityEvent<GameObject, Checkpoint> onCheckpointEnter;
    void OnTriggerEnter(Collider other) // Utilisation du paramètre 'other' au lieu de 'collider'
    {
        // if entering object is tagged as the Player
        CarIdentity carIdentity = other.GetComponent<CarIdentity>();

        // Check if CarIdentity component is not null (i.e., if it's a car)
        if (carIdentity != null)
        {
            // Fire an event giving the entering gameObject and this checkpoint
            onCheckpointEnter.Invoke(other.gameObject, this);
        }
    }
}
