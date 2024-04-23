using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace Nightmare
{
    public class PlayerMovement : PausibleObject
    {
        private CharacterController controller;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float gravity = -9.8f;
        [SerializeField] private float timeSprint = 0.1f;
        [SerializeField] private float manaCost = 2f;

        private float currentSprintTime = 0;
        private bool isSprinting;
        private bool isGrounded;
        private Vector3 playerVelocity;

        Animator anim;                      // Reference to the animator component.

        void Awake ()
        {
            // Set up references.
            anim = GetComponent <Animator> ();
            StartPausible();
        }

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            isGrounded = controller.isGrounded;
            if (isSprinting)
            {
                currentSprintTime += Time.deltaTime;
                if (currentSprintTime >= timeSprint)
                {
                    if (HealthSystem.Instance.isRunOutOfMana())
                    {
                        StopSprint();
                    }
                    else
                    {
                        HealthSystem.Instance.UseMana(manaCost);
                        currentSprintTime = 0;
                    }
                }
            }
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void FixedUpdate ()
        {

        }

        public void StartSprint()
        {
            BaseInstance.Instance.StartSprint();
            isSprinting = true;
        }

        public void StopSprint()
        {
            BaseInstance.Instance.StopSprint();
            isSprinting = false;
        }

        public void ProcessMove (Vector2 input)
        {
            if (isPaused)
                return;

            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            controller.Move(BaseInstance.Instance.currentSpeed * Time.deltaTime * transform.TransformDirection(moveDirection));

            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
            controller.Move(playerVelocity * Time.deltaTime);

            Turning();
            Animating(input.x, input.y);

        }

        public void Jump()
        {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
            }
        }


        void Turning ()
        {

        }


        void Animating (float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool ("IsWalking", walking);
        }
    }
}