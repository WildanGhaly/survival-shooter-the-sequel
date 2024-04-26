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

        [SerializeField] private float shotgunRange = 30f;
        [SerializeField] private float swordRange = 3f;
        [SerializeField] private float rifleRange = 100f;

        [SerializeField] private GameObject rifle;
        [SerializeField] private GameObject shotgun;
        [SerializeField] private GameObject sword;

        [Header("Shotgun Close Range")]
        [SerializeField] private float closeRangeMultiplier = 1f;
        [Tooltip("Effective distance for maximum damage at close range")]
        [SerializeField] private float closeRangeDistance = 5f;

        [Header("Shotgun Mid Range")]
        [SerializeField] private float midRangeMultiplier = 0.5f;
        [Tooltip("Effective distance for reduced damage at mid range")]
        [SerializeField] private float midRangeDistance = 10f;

        [Header("Shotgun Long Range")]
        [SerializeField] private float longRangeMultiplier = 0.2f;
        [Tooltip("Effective distance for minimum damage at long range")]
        [SerializeField] private float longRangeDistance = 30f;

        [SerializeField] private LayerMask layer;

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

        AudioSource rifleAudio;
        AudioSource shotgunAudio;
        AudioSource swordAudio;

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
            
            gunLight = GetComponent<Light> ();
            //faceLight = GetComponentInChildren<Light> ();

            rifleAudio = rifle.GetComponent<AudioSource>();
            shotgunAudio = shotgun.GetComponent<AudioSource>();
            swordAudio = sword.GetComponent<AudioSource>();

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
                    ActivateWeapon(weaponId);
                    return;
                case 2:
                    timeBetweenBullets = shotgunTimeBetweenBullets;
                    weaponId = 2;
                    ActivateWeapon(weaponId);
                    return;
                case 3:
                    timeBetweenBullets = swordTimeBetweenBullets;
                    weaponId = 3;
                    ActivateWeapon(weaponId);
                    return;
                default:
                    timeBetweenBullets = rifleTimeBetweenBullets;
                    weaponId = 1;
                    ActivateWeapon(weaponId);
                    return;
            }
        }

        private void ActivateWeapon(int weaponid)
        {
            switch (weaponid)
            {
                case 1:
                    rifle.SetActive(true);
                    shotgun.SetActive(false);
                    sword.SetActive(false);
                    return;
                case 2:
                    rifle.SetActive(false);
                    shotgun.SetActive(true);
                    sword.SetActive(false);
                    return;
                case 3:
                    rifle.SetActive(false);
                    shotgun.SetActive(false);
                    sword.SetActive(true);
                    return;
                default:
                    rifle.SetActive(true);
                    shotgun.SetActive(false);
                    sword.SetActive(false);
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
            gunLight.enabled = true;
            faceLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();
            gunLine.enabled = true;
            timer = 0f;

            if (weaponId == 1)
            {
                ShootRifle();
                rifleAudio.Play();
            }
            else if (weaponId == 2)
            {
                ShootShotgun();
                shotgunAudio.Play();
            }
            else if (weaponId == 3)
            {
                SwordSlash();
                swordAudio.Play();
            }
        }

        void SwordSlash()
        {
            gunLine.positionCount = 2;
            if (player.GetComponent<InputManager>().isTopDown)
            {
                gunLine.SetPosition(0, transform.position);

                Vector3 shootDirection = transform.forward;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, shootDirection, out hit, swordRange))
                {
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(BaseInstance.Instance.GetGunDamage(), hit.point);
                    }
                }
                gunLine.SetPosition(1, transform.position);
            }
            else
            {
                gunLine.SetPosition(0, transform.position);

                Ray camRay = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;

                if (Physics.Raycast(camRay, out hit, swordRange))
                {
                    shootRay.origin = transform.position;
                    shootRay.direction = (hit.point - transform.position).normalized;

                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(BaseInstance.Instance.GetGunDamage(), hit.point);
                    }
                }
                gunLine.SetPosition(1, transform.position);
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
                    if (Physics.Raycast(shotRay, out hit, shotgunRange, layer))
                    {
                        EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                        float distance = Vector3.Distance(transform.position, hit.point);
                        float multiplier = distance < closeRangeDistance ? closeRangeMultiplier : distance < midRangeDistance ? midRangeMultiplier : distance < longRangeDistance ? longRangeMultiplier : 0;

                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(BaseInstance.Instance.GetGunDamage() * multiplier, hit.point);
                        }
                        gunLine.SetPosition(i * 2 + 1, hit.point);
                    }
                    else
                    {
                        gunLine.SetPosition(i * 2 + 1, shootDirection * shotgunRange);
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
                    Ray shotRay = new (transform.position, shootDirection);

                    gunLine.SetPosition(i * 2, transform.position);

                    if (Physics.Raycast(shotRay, out RaycastHit hit, shotgunRange, layer))
                    {
                        EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                        float distance = Vector3.Distance(transform.position, hit.point);
                        float multiplier = distance < closeRangeDistance ? closeRangeMultiplier : distance < midRangeDistance ? midRangeMultiplier : distance < longRangeDistance ? longRangeMultiplier : 0;
                        
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(BaseInstance.Instance.GetGunDamage() * multiplier, hit.point);
                            
                        }
                        gunLine.SetPosition(i * 2 + 1, hit.point);
                    }
                    else
                    {
                        gunLine.SetPosition(i * 2 + 1, shootDirection * shotgunRange);
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
                if (Physics.Raycast(transform.position, shootDirection, out hit, rifleRange))
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
                    gunLine.SetPosition(1, transform.position + shootDirection * rifleRange);
                }
            }
            else
            {
                gunLine.SetPosition(0, transform.position);

                Ray camRay = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;

                if (Physics.Raycast(camRay, out hit, rifleRange))
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
                    gunLine.SetPosition(1, shootRay.origin + shootRay.direction * rifleRange);
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