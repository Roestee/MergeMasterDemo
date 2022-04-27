using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] detectors;
    [SerializeField] private Vector3[] locations;

    public static GameManager gm;

    private void Awake()
    {
        detectors = new GameObject[15];
        detectors = GameObject.FindGameObjectsWithTag("Detector");
        locations = new Vector3[detectors.Length];

    }

    private void Start()
    {
       
    }

   
}
