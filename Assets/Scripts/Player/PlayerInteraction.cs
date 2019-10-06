using System;
using Interactive;
using UnityEngine;

namespace Player {
    public class PlayerInteraction : MonoBehaviour {
        public bool haveArms;
        public bool canEat;

        public int actionPressed = 0;

        void Start() {
            haveArms = false;
            canEat = false;
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                actionPressed = 1;
            }
        }

        void OnTriggerStay2D(Collider2D collision) {
            ITrigger trigger = collision.GetComponent<ITrigger>();
            if (trigger != null) {
                if (haveArms) {
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        trigger.execute();
                    }
                }
            }
        }
    }
}