using UnityEngine;

namespace Nightmare
{
    public class WizardHealth : EnemyHealth
    {
        [SerializeField] private RectTransform healthBar;
        private float healthBarWidth;
        private float healthMultiplierToUI;
        private bool withHealthBar = false;

        override protected void Awake()
        {
            anim = GetComponent<Animator>();
            enemyAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            if (healthBar != null)
            {
                healthBarWidth = healthBar.sizeDelta.x;
                healthMultiplierToUI = healthBarWidth / startingHealth;
                withHealthBar = true;
            }
        }

        override protected void OnEnable()
        {
            currentHealth = startingHealth;
        }

        override protected void Update()
        {

        }

        override public void TakeDamage(float amount, Vector3 hitPoint)
        {

            if (!IsDead())
            {
                if (withHealthBar)
                {
                    healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x - amount * healthMultiplierToUI, healthBar.sizeDelta.y);
                }
                enemyAudio.Play();
                currentHealth -= amount;

                if (IsDead())
                {
                    Death();
                }
            }

            hitParticles.transform.position = hitPoint;
            hitParticles.Play();
        }

        override protected void Death()
        {
            EventManager.TriggerEvent("Sound", this.transform.position);
            PlayerStatistic.INSTANCE.addKill();
            enemyAudio.clip = deathClip;
            enemyAudio.Play();
            GetComponent<WizardFollow>().enabled = false;
        }
    }
}