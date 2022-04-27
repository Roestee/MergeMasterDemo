using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 startPos;
    private bool isPlaceable = true;

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
            isPlaceable = true;
        }     
    }

    public void ObjectDetect(bool value)
    {
        isPlaceable = value;
    }

}
