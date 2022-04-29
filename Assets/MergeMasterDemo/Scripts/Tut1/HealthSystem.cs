using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 40;

    private int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    public Animator anim;

    private bool isAlive = true;


    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;       
    }

    public int GetHealthValue()
    {
        return maxHealth;
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }

    public void ModifyHealth(int amount)
    {
        if(currentHealth <= 1 && isAlive)
        {
            anim.SetTrigger("Death");

            isAlive = false;

            if (gameObject.CompareTag("Enemy"))
                gameObject.tag = "DeathEnemy";

            if (gameObject.CompareTag("Placed"))
                gameObject.tag = "DeathPlayer";
            return;
        }
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

}
