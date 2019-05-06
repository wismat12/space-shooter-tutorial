using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    private Rigidbody rb;

    public float tumble;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.rb.angularVelocity = Random.insideUnitSphere * tumble;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
