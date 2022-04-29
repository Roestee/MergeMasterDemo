using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridLayout;
    private Grid grid;
    
    [SerializeField] private Tilemap MainTilemap;

    private GameObject[] detectors;
    private Vector3[] locations;

    public GameObject prefab1;

    #region Unity Methods
    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
        detectors = new GameObject[15];
        detectors = GameObject.FindGameObjectsWithTag("Detector");
        locations = new Vector3[detectors.Length];

        for (int i = 0; i < detectors.Length; i++)
        {
            locations[i] = detectors[i].transform.position;
        }
    }
    #endregion

    #region Utils

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    public void InitializeWithObject(GameObject prefab)
    {       
        Vector3 pos = GetAvailableLocation();
        //Check available place = zero;
        if (pos == Vector3.zero)
            return;

        Vector3 position = SnapCoordinateToGrid(pos);

        if (SnapCoordinateToGrid(position) != Vector3.zero)
            Instantiate(prefab, position, Quaternion.identity);
    }

    public void Button1()
    {
        InitializeWithObject(prefab1);
    }

    public Vector3 GetAvailableLocation()
    {
        for (int i = 0; i < detectors.Length; i++)
        {
            while(detectors[i].GetComponent<Detector>().isAvailable) 
            {
                return locations[i];
            }
        }
        return Vector3.zero;
    }
    #endregion
}


