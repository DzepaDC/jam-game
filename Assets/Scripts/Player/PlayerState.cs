using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Serialization;

namespace Player {
    public class PlayerState : MonoBehaviour {
        public int health = 10;
        
        public int food = 100;

        public int tempLevel = 0;
        public int maxTemp = 100;
        public int minTemp = -100;

        public Light2D eyeLeft;
        public Light2D eyeRight;
        public Light2D playerGlow;

        public GameObject eyes;
        
        public PlayerInteraction arms;
        public PlayerMovement pMov;
        public Animator playerAnimator;

        public bool haveLeftEye;
        public bool haveRightEye;
        public bool haveBoots;
        public bool haveArms;
        public bool canEat;

        void Start () {
            haveLeftEye = false;
            haveRightEye = false;
            haveBoots = false;

            arms.enabled = false;
            
            eyeLeft.enabled = false;
            eyeRight.enabled = false;
            playerGlow.enabled = true;
        }

        private void FixedUpdate() {
            Vector2 direction = pMov.GetDirection();
            if (direction != Vector2.zero) {
                var angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
                Quaternion desiredLegsRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                eyes.transform.rotation = Quaternion.Slerp(eyes.transform.rotation, desiredLegsRotation, .25f);
                playerAnimator.speed = direction.x;
            } else {
                playerAnimator.speed = 0f;
            }
        }
        
        public void enableArms() {
            haveArms = true;
            arms.enabled = true;
        }
        public void enableEyes(bool isRight) {
            if (isRight) {
                eyeRight.enabled = true;
                haveRightEye = true;
            } else {
                eyeLeft.enabled = true;
                haveLeftEye = true;
            }
        }
    }
}