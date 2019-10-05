using System;
using UnityEngine;

namespace Player {
    public class PlayerInteraction : MonoBehaviour {
        public bool haveArms;
        public bool canEat;

        public int actionPressed = 0;

        private void Start() {
            haveArms = false;
            canEat = false;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                actionPressed = 1;
            }
        }

        private void FixedUpdate() {
            if (actionPressed > 0) {
                Debug.Log("---> action");
                actionPressed = 0;
            }
            
            // TODO
        }
    }
}