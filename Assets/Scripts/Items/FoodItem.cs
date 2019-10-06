using System;
using Player;
using UnityEngine;

namespace Items {
    public class FoodItem : MonoBehaviour{
        public GameObject moveable;
        private Vector3 initVect;

        public int foodPoints = 10;

        void Start() {
            initVect = moveable.transform.position;
        }
        
        void FixedUpdate() {
            moveable.transform.position = initVect + new Vector3(0, (float) Math.Sin(Time.time * 9f) * 0.1f, 0);
        }
        
        private void OnTriggerEnter2D(Collider2D collision) {
            PlayerState pState = collision.GetComponent<PlayerState>();
            if (pState != null) {
                if (pState.food < pState.maxFood) {
                    bool ate = pState.feed(foodPoints);
                    if (ate) {
                        Destroy(gameObject);
                    } else {
                        Debug.Log("shyachlo");
                    }
                }
            }
        }
    }
}