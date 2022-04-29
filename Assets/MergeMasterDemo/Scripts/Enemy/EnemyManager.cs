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
        PlayerBehavior(); 
    }

    //Damage to player.
    public void DamageTarget()
    {
        player.GetComponent<HealthSystem>().ModifyHealth(damageAmount);
    }

    //Check player condition.
    private void TargetCondCheck()
    {
        //Check enemy is alive or not.
        if (healthSystem.GetIsAlive())
        {
            if (player.CompareTag("Placed"))
            {
                //Turn to closest enemy.
                transform.LookAt(player.transform);
            }
            else
                //Current target is death, find new target.
                FindClosestTarget();
        }
        else
            GetComponent<BoxCollider>().enabled = false;
    }

    //Find closest player.
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

    //Check current game condition.
    private void PlayerBehavior()
    {
        //Check start button pressed.
        if (GameManager.current.roundStart)
        {
            anim.SetBool("isGameStart", true);
            FindClosestTarget();
            TargetCondCheck();
        }
        //Check current stage.
        if (GameManager.current.isStageOver)
        {
            anim.SetBool("isGameStart", false);
        }
    }
}
