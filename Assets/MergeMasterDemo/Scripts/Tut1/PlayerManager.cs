using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float range = 100.0f;
    private Collider[] colliderArray;
    private Animator anim; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        colliderArray = Physics.OverlapSphere(transform.position, range);
        HitTarget();
    }

    private void HitTarget()
    {      
        GameObject enemy = FindClosestTarget();

        if (enemy != null)
        {
            print(enemy.name);
            transform.LookAt(enemy.transform);
            anim.SetTrigger("Attack");
        }           
        else
            colliderArray = Physics.OverlapSphere(transform.position, range);
    }

    private GameObject FindClosestTarget()
    {
        if(colliderArray == null)
        {
            return null;
        }
        foreach (var item in colliderArray)
        {
            if (item.CompareTag("Enemy"))
                return item.gameObject;
        }
        return null;
    }
}
