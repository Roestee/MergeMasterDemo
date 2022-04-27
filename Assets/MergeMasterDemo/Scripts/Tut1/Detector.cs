using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public bool isAvailable;
    

    private void Awake()
    {
        isAvailable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Placed"))
        {
            isAvailable = false;
        }
    }
}
