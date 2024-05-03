using UnityEngine;
using System.Collections;
using Nightmare;
using UnityEngine.SceneManagement;
public class StatueHealth : EnemyHealth
{
    [SerializeField] CanvasGroup canvasGroupText;
    [SerializeField] GameObject player;
    [SerializeField] RectTransform healthBar;
    private float healthBarWidth;
    private float healthMultiplierToUI;

    public GameObject newCam;
    private float fadeDuration = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        healthBarWidth = healthBar.sizeDelta.x;
        healthMultiplierToUI = healthBarWidth / 10000;
    }

    protected override void Death()
    {
        Debug.Log("DESTROYED STATUE");

        GetComponent<Animator>().SetBool("isDestroyed", true);

        player.SetActive(false);

        newCam.SetActive(true);
        
        StartCoroutine(fadeToBlack());
    }

    protected override void OnEnable()
    {
        currentHealth = startingHealth;
    }

    public override void TakeDamage (float amount, Vector3 hitPoint)
    {
        if (!IsDead())
        {
            base.enemyAudio.Play();
            currentHealth -= amount;
            healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x - amount * healthMultiplierToUI, healthBar.sizeDelta.y);

            if (IsDead())
            {
                Death();
            }
        }
    }

    private IEnumerator fadeToBlack()
    {
        float fadeSpeed = (float) 1/fadeDuration;
        // while (canvasGroupBlack.alpha != 1)
        // {
        //     canvasGroupBlack.alpha = Mathf.MoveTowards(canvasGroupBlack.alpha, 1, fadeSpeed * Time.deltaTime);
        //     yield return null;
        // }

        SwitchCamera.Instance.SimpleFade(1, 0.5f);

        while (canvasGroupText.alpha != 1)
        {
            canvasGroupText.alpha = Mathf.MoveTowards(canvasGroupText.alpha, 1, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(5);

        while (canvasGroupText.alpha != 0)
        {
            canvasGroupText.alpha = Mathf.MoveTowards(canvasGroupText.alpha, 0, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0); // TODO: Go to main scene
    }
} 