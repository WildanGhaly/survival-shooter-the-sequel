using UnityEngine;
using System.Collections;
using Nightmare;
using UnityEngine.SceneManagement;
public class StatueHealth : EnemyHealth
{
    [SerializeField] private CanvasGroup canvasGroupBlack;
    [SerializeField] private CanvasGroup canvasGroupText;
    [SerializeField] private GameObject player;

    public GameObject newCam;
    private float fadeDuration = 0.5f;
    
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

            if (IsDead())
            {
                Death();
            }
        }
    }

    private IEnumerator fadeToBlack()
    {
        float fadeSpeed = (float) Mathf.Abs(canvasGroupBlack.alpha-1)/fadeDuration;
        while (canvasGroupBlack.alpha != 1)
        {
            canvasGroupBlack.alpha = Mathf.MoveTowards(canvasGroupBlack.alpha, 1, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        while (canvasGroupText.alpha != 1)
        {
            canvasGroupText.alpha = Mathf.MoveTowards(canvasGroupText.alpha, 1, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(3);

        while (canvasGroupText.alpha != 0)
        {
            canvasGroupText.alpha = Mathf.MoveTowards(canvasGroupText.alpha, 0, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        SceneManager.LoadScene(0); // TODO: Go to main scene
    }
} 