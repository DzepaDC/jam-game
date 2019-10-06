using System;
using Player;
using UnityEngine;

namespace Items {
    public class BrainItem : MonoBehaviour {
        public GameObject moveable;
        private Vector3 initVect;

        void Start() {
            initVect = moveable.transform.position;
        }
        
        void FixedUpdate() {
            moveable.transform.position = initVect + new Vector3(0, (float) Math.Sin(Time.time * 9f) * 0.1f, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            PlayerState pState = collision.GetComponent<PlayerState>();
            if (pState != null) {
                pState.win();

                Destroy(gameObject);
            }
        }
    }
}