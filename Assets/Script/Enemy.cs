﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // move down at 4 m/s
        transform.Translate(Vector3.down * _enemySpeed  * Time.deltaTime);
        if (transform.position.y <= -5.5f)
        { 
            transform.position = new Vector3(Random.Range(-9f, 9f), 7.2f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is player
        // damage the player
        // destroy us
        if(other.tag == "Player")
        {
            // damage player
            Player player = other.transform.GetComponent<Player>();
            // null checking
            if(player != null)
            {
                //other.transform.GetComponent<Player>().Damage();
                player.Damage();
            }
            
            Destroy(this.gameObject);
        }

        // if other is laser
        // laser
        // destory us  
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.addScore();
            }
            Destroy(this.gameObject);
        }
    }
}
