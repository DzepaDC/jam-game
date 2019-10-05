using System;
using Player;
using UnityEngine;

namespace Items {
    public class BootItem : MonoBehaviour {
        public GameObject moveable;
        private Vector3 initVect;

        public float addSpeed = 2f;
        
        void Start() {
            initVect = moveable.transform.position;
        }
        
        void FixedUpdate() {
            moveable.transform.position = initVect + new Vector3(0, (float) Math.Sin(Time.time * 9f) * 0.1f, 0);
        }
        
        private void OnTriggerEnter2D(Collider2D collision) {
            PlayerMovement pMov = collision.GetComponent<PlayerMovement>();
            PlayerState pState = collision.GetComponent<PlayerState>();
            if (pMov != null && pState != null) {
                if (!pState.haveBoots) {
                    pMov.maxSpeed = pMov.maxSpeed + addSpeed;
                    pState.haveBoots = true;
                }

                Destroy(gameObject);
            }
        }
    }
} 