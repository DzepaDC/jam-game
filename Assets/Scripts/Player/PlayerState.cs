using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

namespace Player {
    public class PlayerState : MonoBehaviour {
        public int health = 10;
        
        public int hungerLevel = 0;
        public int maxHungerLevel = 100;
    
        public int tempLevel = 0;
        public int maxTemp = 100;
        public int minTemp = -100;

        public Light2D eyeLeft;
        public Light2D eyeRight;
        public Light2D playerGlow;

        public GameObject eyes;

        public PlayerMovement pMov;

        void Start () {
            eyeLeft.enabled = false;
            eyeRight.enabled = false;
            playerGlow.enabled = false;
        }

        private void FixedUpdate() {
            Vector2 direction = pMov.GetDirection();
            if (direction != Vector2.zero) {
                var angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
                Quaternion desiredLegsRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                eyes.transform.rotation = Quaternion.Slerp(eyes.transform.rotation, desiredLegsRotation, .25f);    
            }
        }

        public void enableEyes(bool isRight) {
            playerGlow.enabled = true;
            if (isRight) {
                eyeRight.enabled = true;
            } else {
                eyeLeft.enabled = true;
            }
        }
    }
}