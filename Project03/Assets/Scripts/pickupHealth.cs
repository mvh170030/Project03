using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupHealth : MonoBehaviour
{
    public int healthToGive;
    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            thePlayer.AddHealth(healthToGive);
            gameObject.SetActive(false);
        }
    }
}
