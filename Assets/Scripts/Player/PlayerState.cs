using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Serialization;

namespace Player {
    public class PlayerState : MonoBehaviour {
        public int health = 100;

        public bool isAlive;
        public int food = 100;
        public int maxFood = 100;
        public int starvePoints = 2;
        public float starveTimeDelta = 5f;
        private float starveTimer;
        

        public int tempLevel = 0;
        public int maxTemp = 100;
        public int minTemp = -100;

        public Light2D eyeLeft;
        public Light2D eyeRight;
        public Light2D eyeBoth;
        public Light2D playerGlow;

        public GameObject eyes;
        
        public PlayerInteraction intertaction;
        public PlayerMovement pMov;
        public Animator playerAnimator;

        public bool haveLeftEye;
        public bool haveRightEye;
        public bool haveBoots;
        public bool haveArms;
        public bool canEat;

        void Start () {
            isAlive = true;
            
            haveLeftEye = false;
            haveRightEye = false;
            haveBoots = false;

            eyeLeft.enabled = false;
            eyeRight.enabled = false;
            eyeBoth.enabled = false;
            playerGlow.enabled = true;

            starveTimer = Time.time;
        }

        private void FixedUpdate() {
            Vector2 direction = pMov.GetDirection();
            if (direction != Vector2.zero) {
                var angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
                Quaternion desiredLegsRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                eyes.transform.rotation = Quaternion.Slerp(eyes.transform.rotation, desiredLegsRotation, .25f);
                

                if (direction.x != 0) {
                    if (direction.x >= 0) {
                        playerAnimator.SetFloat("SpeedRight", 1f);
                        playerAnimator.SetFloat("SpeedLeft", 0f);
                    } else {
                        playerAnimator.SetFloat("SpeedRight", 0f);
                        playerAnimator.SetFloat("SpeedLeft", 1f);
                    }
                } else {
                    if (direction.y >= 0) {
                        playerAnimator.SetFloat("SpeedRight", 1f);
                        playerAnimator.SetFloat("SpeedLeft", 0f);
                    } else {
                        playerAnimator.SetFloat("SpeedRight", 0f);
                        playerAnimator.SetFloat("SpeedLeft", 1f);
                    }
                }
            } else {
                playerAnimator.SetFloat("SpeedRight", 0f);
                playerAnimator.SetFloat("SpeedLeft", 0f);
            }

            float actTime = Time.time; 
            if (actTime - starveTimer > starveTimeDelta) {
                food = food - starvePoints;
                starveTimer = actTime;
                Debug.Log("---> food: "+food);

                if (food <= 0 && isAlive) {
                    die();
                }
            }
        }

        public bool feed(int ff) {
            if (canEat && food < maxFood) {
                food = food + ff;
                if (food > maxFood) {
                    food = maxFood;
                }

                return true;
            }

            return false;
        }
        
        public void enableArms() {
            haveArms = true;
            intertaction.haveArms = true;
            Debug.Log("---> armsEnabled");
        }
        public void enableEyes(bool isRight) {
            if (isRight) {
                eyeRight.enabled = true;
                haveRightEye = true;
            } else {
                eyeLeft.enabled = true;
                haveLeftEye = true;
            }

            if (eyeRight.enabled || eyeLeft.enabled) {
//                playerAnimator.runtimeAnimatorController = plPhase2;
            }

            if (eyeRight.enabled && eyeLeft.enabled) {
                eyeLeft.enabled = false;
                eyeRight.enabled = false;
                eyeBoth.enabled = true;
            }
        }

        public void die() {
            isAlive = false;
            
            pMov.hardStop();
            pMov.enabled = false;
            intertaction.enabled = false;
        }
    }
}