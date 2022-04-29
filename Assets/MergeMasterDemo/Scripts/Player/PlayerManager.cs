using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int damageAmount = -10;

    public string level;

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
        PlayerBehavior();
    }

    //Damage to enemy.
    public void DamageTarget()
    {
        enemy.gameObject.GetComponent<HealthSystem>().ModifyHealth(damageAmount);
    }

    //Check player condition
    private void TargetCondCheck()
    {
        //Check player is alive or not.
        if (healthSystem.GetIsAlive())
        {
            if (enemy.CompareTag("Enemy"))           
                //Turn to closest enemy.
                transform.LookAt(enemy.transform);          
            else
                //Current target is death, find new target.
                FindClosestTarget();
        }
        else
            GetComponent<BoxCollider>().enabled = false;
    }

    //Find closest enemy.
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
