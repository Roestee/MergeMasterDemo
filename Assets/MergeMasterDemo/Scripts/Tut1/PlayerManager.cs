using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int damageAmount = -10;

    private Animator anim;
    private HealthSystem healthSystem;
    private GameObject enemy;

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
            print(enemy.name);
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
        enemy.gameObject.GetComponent<HealthSystem>().ModifyHealth(damageAmount);
    }

    private void HitTarget()
    {
        if (healthSystem.GetIsAlive())
        {
            if (enemy.CompareTag("Placed"))
            {
                print(enemy.name);
                transform.LookAt(enemy.transform);
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
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        enemy = closestEnemy;
    }
}
