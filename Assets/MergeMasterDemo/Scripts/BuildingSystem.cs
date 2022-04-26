using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.MergeMasterDemo.Scripts
{
    public class BuildingSystem : MonoBehaviour
    {
        [Header("TileMap")]
        [SerializeField] private Tilemap MainTileMap;
        [SerializeField] private TileBase whiteTile;

        public static BuildingSystem current;

        public GridLayout gridLayout;

        [Header("Prefabs")]
        public GameObject prefab1;
        public GameObject prefab2;

        private Grid grid;
        private PlaceableObject objectToPlace;

        #region Unity Methods
        private void Awake()
        {
            current = this;
            grid = gridLayout.gameObject.GetComponent<Grid>();
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

        #endregion

        #region Placement

        public void InitializeWithObject(GameObject prefab)
        {
            Vector3 position = SnapCoordinateToGrid(Vector3.zero);

            GameObject obj = Instantiate(prefab, position, Quaternion.identity);
            objectToPlace = obj.GetComponent<PlaceableObject>();
            obj.AddComponent<ObjectDrag>();
        }
        #endregion

        #region Buttons
        public void SpawnObject1()
        {
            InitializeWithObject(prefab1);
        }

        public void SpawnObject2()
        {
            InitializeWithObject(prefab2);
        }
        #endregion
    }
}

