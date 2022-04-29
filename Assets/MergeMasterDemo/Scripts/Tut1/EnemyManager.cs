using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int damageAmount = -10;

    private GameObject player;
    private Animator anim;
    private HealthSystem healthSystem;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();
    }
    private void Update()
    {
        if (GameManager.current.roundStart)
        {
            StartAnim();
            FindClosestTarget();
            HitTarget();
        }
        if (GameManager.current.isStageOver)
        {
            anim.SetBool("isGameStart", false);
        }
    }
    public void StartAnim()
    {
        if (!anim.GetBool("isGameStart"))
            anim.SetBool("isGameStart", true);
    }

    public void DamageTarget()
    {
        player.GetComponent<HealthSystem>().ModifyHealth(damageAmount);
    }

    private void HitTarget()
    {
        if (healthSystem.GetIsAlive())
        {
            if (player.CompareTag("Placed"))
            {
                print(player.name);
                transform.LookAt(player.transform);
            }
            else
                FindClosestTarget();
        }
        else
            GetComponent<BoxCollider>().enabled = false;
    }

    private void FindClosestTarget()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Placed");

        foreach (GameObject currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        player = closestEnemy;
    }
   
}
