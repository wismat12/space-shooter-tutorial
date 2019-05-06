using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ; //tile size along z axes

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * -scrollSpeed, tileSizeZ); //modulo time % size
        transform.position = startPosition + Vector3.forward * newPosition; //forward's (1,1,1)
    }
}
