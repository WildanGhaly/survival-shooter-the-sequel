using UnityEngine;
using UnityEngine.Events;
using System.Text;
using UnitySampleAssets.CrossPlatformInput;

namespace Nightmare
{
    public class PlayerShooting : PausibleObject
    {
        public float timeBetweenBullets = 0.15f;
        [SerializeField] private float rifleTimeBetweenBullets = 0.15f;
        [SerializeField] private float shotgunTimeBetweenBullets = 1f;
        [SerializeField] private float swordTimeBetweenBullets = 0.5f;

        public float range = 100f;
        public GameObject grenade;
        public float grenadeSpeed = 200f;
        public float grenadeFireDelay = 0.5f;
        public Camera cam;
        public GameObject player;

        public bool isFiringBullet { get; private set; }
        public bool isFiringGranat { get; private set; }

        [SerializeField] private int weaponId = 1;

        float timer;
        Ray shootRay = new Ray();
        ParticleSystem gunParticles;
        LineRenderer gunLine;
        AudioSource gunAudio;
        Light gunLight;
		public Light faceLight;
        float effectsDisplayTime = 0.2f;
        int grenadeStock = 99;

        [SerializeField] private int shotgunBulletCount = 10;
        [SerializeField] private float shotgunMaxSpreadAngle = 20f;
  
        private UnityAction listener;

        void Awake ()
        {
            // Set up the references.
            gunParticles = GetComponent<ParticleSystem> ();
            gunLine = GetComponent <LineRenderer> ();
            gunAudio = GetComponent<AudioSource> ();
            gunLight = GetComponent<Light> ();
			//faceLight = GetComponentInChildren<Light> ();

            AdjustGrenadeStock(0);

            listener = new UnityAction(CollectGrenade);

            EventManager.StartListening("GrenadePickup", CollectGrenade);

            StartPausible();
        }


        void OnDestroy()
        {
            EventManager.StopListening("GrenadePickup", CollectGrenade);
            StopPausible();
        }

        void Update ()
        {
            if (isPaused)
                return;

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            if (timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                // If the Fire1 button is being press and it's time to fire...
                if (isFiringGranat && grenadeStock > 0)
                {
                    // ... shoot a grenade.
                    ShootGrenade();
                }

                // If the Fire1 button is being press and it's time to fire...
                else if (isFiringBullet)
                {
                    // ... shoot the gun.
                    Shoot();
                }
            }
            
            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            if(timer >= rifleTimeBetweenBullets * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects ();
            }
        }

        public void ChangeWeapon(int id)
        {
            switch (id)
            {
                case 1:
                    timeBetweenBullets = rifleTimeBetweenBullets;
                    weaponId = 1;
                    return;
                case 2:
                    timeBetweenBullets = shotgunTimeBetweenBullets;
                    weaponId = 2;
                    return;
                case 3:
                    timeBetweenBullets = swordTimeBetweenBullets;
                    weaponId = 3;
                    return;
                default:
                    timeBetweenBullets = rifleTimeBetweenBullets;
                    weaponId = 1;
                    return;
            }
        }

        public void StartFire()
        {
            isFiringBullet = true;
        }

        public void StopFire()
        {
            isFiringBullet = false;
        }

        public void StartThrowGranat()
        {
            isFiringGranat = true;
        }

        public void StopThrowGranat()
        {
            isFiringGranat = false;
        }


        public void DisableEffects ()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
			faceLight.enabled = false;
            gunLight.enabled = false;
        }

        void Shoot()
        {
            gunAudio.Play();
            gunLight.enabled = true;
            faceLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();
            gunLine.enabled = true;
            timer = 0f;

            if (weaponId == 1)
            {
                ShootRifle();
            }
            else if (weaponId == 2)
            {
                ShootShotgun();
            }
        }

        void ShootShotgun()
        {
            gunLine.positionCount = shotgunBulletCount * 2;
            if (player.GetComponent<InputManager>().isTopDown)
            {
                for (int i = 0; i < shotgunBulletCount; i++)
                {
                    float angle = shotgunMaxSpreadAngle * (i - shotgunBulletCount / 2) / (shotgunBulletCount / 2);
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

                    Vector3 shootDirection = rotation * transform.forward;
                    Ray shotRay = new Ray(transform.position, shootDirection);
                    RaycastHit hit;

                    gunLine.SetPosition(i * 2, transform.position);
                    if (Physics.Raycast(shotRay, out hit, range))
                    {
                        EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(BaseInstance.Instance.GetGunDamage(), hit.point);
                            gunLine.SetPosition(i * 2 + 1, hit.point);
                        }
                        else
                        {
                            gunLine.SetPosition(i * 2 + 1, shootDirection * range);
                        }
                    }
                    else
                    {
                        gunLine.SetPosition(i * 2 + 1, shootDirection * range);
                    }
                }
            }
            else
            {
                for (int i = 0; i < shotgunBulletCount; i++)
                {
                    float horizontalAngle = shotgunMaxSpreadAngle * (Random.value - 0.5f) * 2;
                    float verticalAngle = shotgunMaxSpreadAngle * (Random.value - 0.5f) * 2;

                    Quaternion horizontalRotation = Quaternion.AngleAxis(horizontalAngle, Vector3.up);
                    Quaternion verticalRotation = Quaternion.AngleAxis(verticalAngle, Vector3.right);
                    Quaternion combinedRotation = horizontalRotation * verticalRotation;

                    Vector3 shootDirection = combinedRotation * cam.transform.forward;
                    Ray shotRay = new Ray(transform.position, shootDirection);
                    RaycastHit hit;

                    gunLine.SetPosition(i * 2, transform.position);

                    if (Physics.Raycast(shotRay, out hit, range))
                    {
                        EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            Debug.Log("Enemy hit damage: " + BaseInstance.Instance.GetGunDamage());
                            enemyHealth.TakeDamage(BaseInstance.Instance.GetGunDamage(), hit.point);
                            gunLine.SetPosition(i * 2 + 1, hit.point);
                        }
                        else
                        {
                            gunLine.SetPosition(i * 2 + 1, shootDirection * range);
                        }
                        
                    }
                    else
                    {
                        gunLine.SetPosition(i * 2 + 1, shootDirection * range);
                    }
                }
            }
        }


        void ShootRifle()
        {
            gunLine.positionCount = 2;
            if (player.GetComponent<InputManager>().isTopDown)
            {
                gunLine.SetPosition(0, transform.position);

                Vector3 shootDirection = transform.forward; 
                RaycastHit hit;
                if (Physics.Raycast(transform.position, shootDirection, out hit, range))
                {
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(BaseInstance.Instance.GetGunDamage(), hit.point);
                    }
                    gunLine.SetPosition(1, hit.point);
                }
                else
                {
                    gunLine.SetPosition(1, transform.position + shootDirection * range);
                }
            }
            else
            {
                gunLine.SetPosition(0, transform.position);

                Ray camRay = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;

                if (Physics.Raycast(camRay, out hit, range))
                {
                    shootRay.origin = transform.position;
                    shootRay.direction = (hit.point - transform.position).normalized;

                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(BaseInstance.Instance.GetGunDamage(), hit.point);
                    }

                    gunLine.SetPosition(1, hit.point);
                }
                else
                {
                    shootRay.origin = transform.position;
                    shootRay.direction = camRay.direction;
                    gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
                }
            }
            
        }



        private void ChangeGunLine(float midPoint)
        {
            AnimationCurve curve = new AnimationCurve();

            curve.AddKey(0f, 0f);
            curve.AddKey(midPoint, 0.5f);
            curve.AddKey(1f, 1f);

            gunLine.widthCurve = curve;
        }

        public void CollectGrenade()
        {
            AdjustGrenadeStock(1);
        }

        private void AdjustGrenadeStock(int change)
        {
            grenadeStock += change;
            GrenadeManager.grenades = grenadeStock;
        }

        void ShootGrenade()
        {
            AdjustGrenadeStock(-1);
            timer = timeBetweenBullets - grenadeFireDelay;
            GameObject clone = PoolManager.Pull("Grenade", transform.position, Quaternion.identity);
            EventManager.TriggerEvent("ShootGrenade", grenadeSpeed * transform.forward);
            //GameObject clone = Instantiate(grenade, transform.position, Quaternion.identity);
            //Grenade grenadeClone = clone.GetComponent<Grenade>();
            //grenadeClone.Shoot(grenadeSpeed * transform.forward);
        }
    }
}