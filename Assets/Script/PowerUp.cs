using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   
    private float _tripleShotSpeed = 3.0f;

    // create IDS for powerups
    // 0 for tripleshot
    // 1 for speed boost
    // 2 for sheild
    [SerializeField]
    private int powerUpID;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _tripleShotSpeed * Time.deltaTime);

        if(transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // communicate with the player script
            // handle the component i want
            // assign the handle to the component
            Player player = other.transform.GetComponent<Player>();
            // null condition
            if(player != null)
            {
                // if powerUpID = 0 -> triple shot power
                // if powerUpID = 1 -> speed boost
                // if powerUpID = 2 -> sheild
                switch(powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.speedBoostActive();
                        break;
                    case 2:
                        player.sheildActive();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
