using System;
using Common;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

namespace Player {
    public class PlayerState : MonoBehaviour {
        public HudScript hud;

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

        public int quitPressed = 0;
        public int restartPressed = 0;
        public bool wins = false;
        
        void Start () {
            hud.setFinal("");
            hud.setMessage("i can't do nothing!", 2f);
            isAlive = true;
            wins = false;
            
            
            haveLeftEye = false;
            haveRightEye = false;
            haveBoots = false;

            eyeLeft.enabled = false;
            eyeRight.enabled = false;
            eyeBoth.enabled = false;
            playerGlow.enabled = true;

            starveTimer = Time.time;
        }

        private void Update() {
            if (!isAlive || wins) {
                if (Input.GetKeyDown(KeyCode.Q)) {
                    quitPressed = 1;
                }

                if (Input.GetKeyDown(KeyCode.R)) {
                    restartPressed = 1;
                }
            }
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
            
            hud.setFood(food);

            if (quitPressed > 0) {
                Application.Quit();
            }

            if (restartPressed > 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        public bool feed(int ff) {
            if (!canEat) {
                hud.setMessage("i have no mouth!");
            }
            
            if (canEat && food < maxFood) {
                food = food + ff;
                if (food > maxFood) {
                    food = maxFood;
                }

                hud.setMessage("got "+ff+" food");
                
                return true;
            }
            
            return false;
        }
        
        public void enableArms() {
            haveArms = true;
            intertaction.haveArms = true;
            hud.setMessage("i have arms!");
            checkAnimator();
        }

        public void msgTransmit(String msg) {
            hud.setMessage(msg);
        }

        public void enableMouth() {
            canEat = true;
            checkAnimator();
            hud.setMessage("i can eat!");
        }
        
        public void enableEyes(bool isRight) {
            if (isRight) {
                hud.setMessage("i have right eye!");
                eyeRight.enabled = true;
                haveRightEye = true;
            } else {
                hud.setMessage("i have left eye!");
                eyeLeft.enabled = true;
                haveLeftEye = true;
            }

            if (eyeRight.enabled && eyeLeft.enabled) {
                eyeLeft.enabled = false;
                eyeRight.enabled = false;
                eyeBoth.enabled = true;
                hud.setMessage("i have both eyes!");
            }
            
            checkAnimator();
        }

        public void die() {
            isAlive = false;
            
            pMov.hardStop();
            pMov.enabled = false;
            intertaction.enabled = false;
            
            hud.setMessage("bbrbrbeqkrehhhhrlt!", 10f);
            playerAnimator.SetInteger("Explode", 1);

            hud.setFinal("You're dead.\n" +
                         "Press R to restart\n"+
                         "Press Q to exit");
        }

        public void win() {
            pMov.hardStop();
            pMov.enabled = false;
            intertaction.enabled = false;
            
            playerAnimator.SetFloat("SpeedRight", 0f);
            playerAnimator.SetFloat("SpeedLeft", 0f);
            
            hud.setMessage("now i'm a alive!", 10f);
            hud.setFinal("You WIN.\n" +
                         "Press R to restart\n"+
                         "Press Q to exit");

            wins = true;
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

            if (tmpState >= 0) {
                actualState = tmpState;
                playerAnimator.runtimeAnimatorController = states[actualState];
            }
        }
    }
}