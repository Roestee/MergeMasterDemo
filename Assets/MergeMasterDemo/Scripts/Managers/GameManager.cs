using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    [Header("Health Update Speed")]
    [SerializeField] private float updateSpeedSeconds = 0.2f;

    [Header("Foregrounds")]
    [SerializeField] private Image foregroundImageEnemy;
    [SerializeField] private Image foregroundImagePlayer;

    [Header("Panels")]
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject restartPanel;
    [SerializeField] TextMeshProUGUI coinText;


    public bool roundStart { get; private set; }
    public bool isStageOver { get; private set; }
    public int coin { get; private set; }

    //Enemys and players on the scene.
    private GameObject[] enemys;
    private GameObject[] players;

    //Health variables.
    private int playersMaxHealth;
    private int playersCurrentHealth;
    private int enemysMaxHealth;
    private int enemysCurrentHealth;

    private void Awake()
    {
        current = this;
        roundStart = false;
        isStageOver = false;
        coin = 200;
        coinText.text = coin.ToString();
    }

    private void Update()
    {
        if (roundStart)
        {
            //Enemys and player total health smoothly update on screen.
            StartCoroutine(ChangeToPct(CalculateHealthPerc(enemysCurrentHealth, enemysMaxHealth), foregroundImageEnemy));
            StartCoroutine(ChangeToPct(CalculateHealthPerc(playersCurrentHealth, playersMaxHealth), foregroundImagePlayer));

            //Calculate total healths on round start.
            CalculateHealths();

            //Check if all enemys die, Activete win canvas.
            if (enemysCurrentHealth <= 0)
            {
                isStageOver = true;
                roundStart = false;
                SetUI(false, false, true, false);
            }

            //Check if all players die, Activete lose canvas.
            if (playersCurrentHealth <= 0)
            {
                isStageOver = true;
                roundStart = false;
                SetUI(false, false, false, true);
            }
        }
    }

    //Start button.
    public void StartFightButton()
    {
        roundStart = true;
        SetUI(false, true, false, false);
        CalculateHealths();
        enemysMaxHealth = enemysCurrentHealth;
        playersMaxHealth = playersCurrentHealth;
    }

    //Quit button.
    public void QuitButton()
    {
        Application.Quit();
    }

    //Next Level button.
    public void NexLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Calculate coin
    public void CalculateCoin(int value)
    {
        coin += value;
        coinText.text = coin.ToString();
    }

    //Change UI Panel.
    private void SetUI(bool startPanel, bool gamePanel, bool endPanel, bool restartPanel)
    {
        this.startPanel.SetActive(startPanel);
        this.gamePanel.SetActive(gamePanel);
        this.endPanel.SetActive(endPanel);
        this.restartPanel.SetActive(restartPanel);
    }

    //Find all players and enemys on the scene and calculate healts.
    private void CalculateHealths()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        players = GameObject.FindGameObjectsWithTag("Placed");

        enemysCurrentHealth = enemys.Length;
        playersCurrentHealth = players.Length;
    }

    //Calculate health perc. for UI.
    private float CalculateHealthPerc(int x, int y)
    {
        float healthPct = (float)x / (float)y;
        return healthPct;
    }

    //Change health on UI smoothly.
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
}
