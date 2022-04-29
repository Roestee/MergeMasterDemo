using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Foreground")]
    [SerializeField] private Image foregroundImage;
    [Header("Health Update Speed")]
    [SerializeField] private float updateSpeedSeconds = 0.2f;

    private void Awake()
    {
        GetComponentInParent<HealthSystem>().OnHealthPctChanged += HandleHealthChanged;
    }

    //Turn UI Health to camera.
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while(elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
    }
}
