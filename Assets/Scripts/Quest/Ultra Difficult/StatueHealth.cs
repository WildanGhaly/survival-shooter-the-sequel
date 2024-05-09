using UnityEngine;
using System.Collections;
using Nightmare;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StatueHealth : EnemyHealth
{
    [SerializeField] CanvasGroup canvasGroupText;
    [SerializeField] GameObject player;
    [SerializeField] RectTransform healthBar;
    [SerializeField] CanvasGroup canvasGroupBlack;
    [SerializeField] GameObject enemyManager;
    private float healthBarWidth;
    private float healthMultiplierToUI;

    public GameObject newCam;
    private float fadeDuration = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        healthBarWidth = healthBar.sizeDelta.x;
        healthMultiplierToUI = healthBarWidth / 5000;
    }

    protected override void Death()
    {
        GameManager.INSTANCE.addCoin(100);
        GameManager.INSTANCE.addPoint(150);
        GameManager.INSTANCE.updateCurrentQuestID(7);
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

    private IEnumerator genocideEnemies()
    {
        while(GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            yield return null;
        }
    }

    private IEnumerator fadeToBlack()
    {
        enemyManager.SetActive(false);

        StartCoroutine(genocideEnemies());

        float fadeSpeed = (float) 1/fadeDuration;
        while (canvasGroupBlack.alpha != 1)
        {
            canvasGroupBlack.alpha = Mathf.MoveTowards(canvasGroupBlack.alpha, 1, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        // SwitchCamera.Instance.SimpleFade(1, 0.5f);
        // yield return new WaitForSeconds(1);

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

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(3); // TODO: Go to main scene
    }
} 