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

        public AnimatorOverrideController[] states;
        private int actualState = -1;

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
            checkAnimator();
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
            
            checkAnimator();
        }

        public void die() {
            isAlive = false;
            
            pMov.hardStop();
            pMov.enabled = false;
            intertaction.enabled = false;
            
            playerAnimator.SetInteger("Explode", 1);
        }

        public void checkAnimator() {
            bool haveOneEye = (haveLeftEye && !haveRightEye) || (haveRightEye && !haveLeftEye);
            bool haveBothEyes = haveLeftEye && haveRightEye;
            int tmpState = actualState;
            
            if (haveOneEye) {
                if (haveArms) {
                    if (canEat && haveBoots) {
                        tmpState = 7;
                    } else if (canEat) {
                        tmpState = 3;
                    } else if (haveBoots) {
                        tmpState = 5;
                    } else {
                        tmpState = 1;
                    }
                } else {
                    tmpState = 0;
                }
            }

            if (haveBothEyes) {
                if (haveArms) {
                    if (haveBoots && canEat) {
                        tmpState = 8;
                    } else if (haveBoots) {
                        tmpState = 6;
                    } else if (canEat) {
                        tmpState = 4;
                    } else {
                        tmpState = 2;
                    }
                }
            }

            actualState = tmpState;
            playerAnimator.runtimeAnimatorController = states[actualState];
        }
    }
}