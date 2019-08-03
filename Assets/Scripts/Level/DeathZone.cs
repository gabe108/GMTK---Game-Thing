using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDeath playerDeath = other.gameObject.GetComponent<PlayerDeath>();
        
        // if the colliding object is the player...
        if (playerDeath != null)
            playerDeath.Die(); // kill them
    }
}
