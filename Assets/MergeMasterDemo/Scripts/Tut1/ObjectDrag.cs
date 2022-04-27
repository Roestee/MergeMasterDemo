using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 startPos;
    private bool objectIsAvailable;
    public bool isPlaceable = true;
    public bool isEmpty = true;

    private void OnMouseDown()
    {
        startPos = transform.position;
        offset = transform.position - BuildingSystem.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        float x = Mathf.Clamp(pos.x, -9.0f, 9.0f);
        float z = Mathf.Clamp(pos.z, -15.0f, -5f);
        pos = new Vector3(x, pos.y, z);
        transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);    
    }

    private void OnMouseUp()
    {
        if (!isPlaceable)
        {
            transform.position = startPos;
        }
        else
        {
            gameObject.transform.tag = "Placed";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case"Detector":
                isPlaceable = false;
                other.GetComponent<Detector>().isAvailable = false;
                break;
            case "Placeable":
                isPlaceable = true;
                break;
        }                     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            isPlaceable = true;
            other.GetComponent<Detector>().isAvailable = true;
        }
    }

}
