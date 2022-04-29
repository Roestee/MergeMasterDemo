using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    [SerializeField]
    private Image foregroundImageEnemy, foregroundImagePlayer;

    [SerializeField]
    private float updateSpeedSeconds = 0.2f;

    [SerializeField] GameObject startPanel, gamePanel, endPanel, restartPanel;

    public bool roundStart { get; private set;}
    public bool isStageOver{ get; private set; }


    private GameObject[] enemys;
    private GameObject[] players;

    private int playersMaxHealth;
    private int playersCurrentHealth;
    private int enemysMaxHealth;
    private int enemysCurrentHealth;

    private void Awake()
    {
        current = this;
        roundStart = false;
        isStageOver = false;
    }
    private void Update()
    {
        if (roundStart)
        {
            StartCoroutine(ChangeToPct(CalculateHealthPerc(enemysCurrentHealth, enemysMaxHealth), foregroundImageEnemy));
            StartCoroutine(ChangeToPct(CalculateHealthPerc(playersCurrentHealth, playersMaxHealth), foregroundImagePlayer)); 

            CalculateHealths();

            if(enemysCurrentHealth <= 0)
            {
                isStageOver = true;
                roundStart = false;
                SetUI(false, false, true, false);
            }
            if(playersCurrentHealth <= 0)
            {
                isStageOver = true;
                roundStart = false;
                SetUI(false, false, false, true);

            }
        }
    }

    public void StartFightButton()
    {
        roundStart = true;
        SetUI(false, true, false, false);
        CalculateHealths();
        enemysMaxHealth = enemysCurrentHealth;
        playersMaxHealth = playersCurrentHealth;
    }

    private void SetUI(bool startPanel, bool gamePanel, bool endPanel, bool restartPanel)
    {
        this.startPanel.SetActive(startPanel);
        this.gamePanel.SetActive(gamePanel);
        this.endPanel.SetActive(endPanel);
        this.restartPanel.SetActive(restartPanel);
    }

    private void CalculateHealths()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        players = GameObject.FindGameObjectsWithTag("Placed");

        enemysCurrentHealth = enemys.Length;
        playersCurrentHealth = players.Length;
    }

    private float CalculateHealthPerc(int x, int y)
    {
        float healthPct = (float)x / (float)y;
        return healthPct;
    }

    private IEnumerator ChangeToPct(float pct, Image foregroundImage)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void NexLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
