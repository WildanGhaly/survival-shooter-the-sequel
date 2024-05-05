using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Nightmare
{
    public class YodaHealth : EnemyHealth
    {
        [SerializeField] private float targetSink = 4f, waitBeforeSink = 3f;
        
        override protected void Awake()
        {
            anim = GetComponent<Animator>();
            enemyAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
        }

        override protected void OnEnable()
        {
            currentHealth = startingHealth;
        }

        override protected void Update()
        {

        }

        public override bool IsDead()
        {
            return base.IsDead();
        }

        public override void TakeDamage(float amount, Vector3 hitPoint)
        {
            base.TakeDamage(amount, hitPoint);
            Debug.Log("Damadged: " + amount);
        }

        override protected void Death()
        {
            base.Death();
            anim.SetBool("Die", true);
            GetComponent<YodaFollow>().enabled = false;
            StartCoroutine(StartSink());
        }

        IEnumerator StartSink()
        {
            yield return new WaitForSeconds(waitBeforeSink);
            Vector3 position = transform.position;
            GetComponent<NavMeshAgent>().enabled = false;
            yield return new WaitForSeconds(waitBeforeSink);
            while (targetSink > 0)
            {
                yield return null;
                float sinkAmount = Time.deltaTime * sinkSpeed;
                targetSink -= sinkAmount;
                position.y -= sinkAmount;
                transform.position = position;
            }
            Destroy(gameObject);
        }
      
    }
}