using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    //detroying any bojects that leave the triggers volume
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
