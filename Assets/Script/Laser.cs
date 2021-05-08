using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // speed of laser
    [SerializeField]
    private float _laserSpeed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // translate laser up infintely 
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        // if laser position is gretaer than 8 destroy the object 
        if(transform.position.y > 8f)
        {
            // check if this object has a parent
            // if it has then what to do --> destroy the parent
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
