using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action<float> OnHealthPctChanged = delegate { };

    [SerializeField] private int maxHealth = 40;
    [SerializeField] private int coin;

    private Animator anim;
    private bool isAlive = true;
    private int currentHealth;

    private void OnEnable()
    {
        anim = this.GetComponent<Animator>();
        currentHealth = maxHealth;       
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }

    //Change health for this gameobject.
    public void ModifyHealth(int amount)
    {
        //Check current health.
        if(currentHealth <= 1 && isAlive)
        {
            anim.SetTrigger("Death");      
            isAlive = false;

            if (gameObject.CompareTag("Enemy"))
            {
                gameObject.tag = "DeathEnemy";
                GameManager.current.CalculateCoin(coin);
            }

            if (gameObject.CompareTag("Placed"))
                gameObject.tag = "DeathPlayer";
            return;
        }
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

}
