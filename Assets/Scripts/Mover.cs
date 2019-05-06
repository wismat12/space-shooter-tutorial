using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * speed;  //forward along z axis 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
