using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject level2Prefab;
    [SerializeField] GameObject level3Prefab;
    [SerializeField] GameObject level4Prefab;

    public bool isPlaceable = true;

    private Vector3 offset;
    private Vector3 startPos;
    private bool isRoundStart;
    private string levelCol;
    private GameObject levelOther;

    private void Start()
    {
        if (GetComponent<PlayerManager>() != null)
            levelCol = GetComponent<PlayerManager>().level;
        if (GetComponent<WarriorManager>() != null)
            levelCol = GetComponent<WarriorManager>().level;
    }

    private void Update()
    {
        isRoundStart = GameManager.current.roundStart;
    }
    private void OnMouseDown()
    {
        if (isRoundStart)
        {
            return;
        }
        startPos = transform.position;
        offset = transform.position - BuildingSystem.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (isRoundStart)
        {
            return;
        }

        Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        float x = Mathf.Clamp(pos.x, -9.0f, 9.0f);
        float z = Mathf.Clamp(pos.z, -15.0f, -5f);
        pos = new Vector3(x, pos.y, z);
        transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
    }

    private void OnMouseUp()
    {
        if (isRoundStart)
        {
            return;
        }
        if (!isPlaceable)
        {
            string otherTag = levelOther.GetComponent<PlayerManager>().level;
            switch (otherTag)
            {
                case "Level1":
                    if (levelCol == "Level1")
                    {
                        Vector3 pos = transform.position;
                        Destroy(this.gameObject);
                        Destroy(levelOther);
                        Instantiate(level2Prefab, pos, Quaternion.identity);
                    }
                    break;
                case "Level2":
                    if (levelCol == "Level2")
                    {
                        Vector3 pos = transform.position;
                        Destroy(this.gameObject);
                        Destroy(levelOther);
                        Instantiate(level3Prefab, pos, Quaternion.identity);
                    }
                    break;
                case "Level3":
                    if (levelCol == "Level3")
                    {
                        Vector3 pos = transform.position;
                        Destroy(this.gameObject);
                        Destroy(levelOther);
                        Instantiate(level4Prefab, pos, Quaternion.identity);
                    }
                    break;
            }
            transform.position = startPos;
        }
    }

    private void OnTriggerStay(Collider other)
    {  
        if (other.CompareTag("Detector"))
        {
            other.GetComponent<Detector>().isAvailable = false;
        }
        if (other.CompareTag("Placed"))
        {
            levelOther = other.gameObject;
            isPlaceable = false;
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
