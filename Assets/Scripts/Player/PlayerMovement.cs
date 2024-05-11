﻿using UnityEngine;
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
        [SerializeField] private float sprintSpeedUpMultiplier = 20f;
        [SerializeField] AudioSource walkAud;
        [SerializeField] AudioSource runAud;
        [SerializeField] AudioSource jumpAud;

        private float currentSprintTime = 0;
        private bool isSprinting;
        private bool isGrounded;
        private Vector3 playerVelocity;
        public float speedModifier = 1f;
        private bool isTriggered = false;

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
            if (!isGrounded)
            {
                walkAud.Stop();
                isTriggered = false;
                if (isSprinting)
                {
                    runAud.Stop();
                }
            } else
            {
                if (!runAud.isPlaying && isSprinting)
                {
                    runAud.Play();
                }
            }
        }

        void OnDestroy()
        {
            StopPausible();
        }

        public void StartSprint()
        {
            if ((playerVelocity.x != 0 || playerVelocity.z != 0) && !isSprinting)
            {
                BaseInstance.Instance.AddMultiplierSpeed(sprintSpeedUpMultiplier / 100);
                isSprinting = true;
                isTriggered = false;
                walkAud.Stop();
                runAud.Play();
            }
        }

        public void StopSprint()
        {
            if (isSprinting)
            {
                BaseInstance.Instance.AddMultiplierSpeed(-sprintSpeedUpMultiplier / 100);
                isSprinting = false;
                runAud.Stop();
            }
        }

        public void ProcessMove (Vector2 input)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            playerVelocity.x = input.x;
            playerVelocity.z = input.y;
            controller.Move(BaseInstance.Instance.GetCurrentSpeed() * Time.deltaTime * transform.TransformDirection(moveDirection));

            playerVelocity.y += gravity * Time.deltaTime * speedModifier;
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
            controller.Move(playerVelocity * Time.deltaTime);

            Turning();
            Animating(input.x, input.y);

            if (input != Vector2.zero)
            {
                if (!isTriggered)
                {
                    walkAud.Play();
                }
                isTriggered = true;
            } else
            {
                walkAud.Stop();
                isTriggered = false;
            }
        }

        public void Jump()
        {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
                jumpAud.Play();
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