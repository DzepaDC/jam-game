using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common {
    public class HudScript : MonoBehaviour{
        public Text food;
        public Text messages;
        public Text final;

        public float msgDelta = 1f;
        public float lastMsgTime;

        private void Start() {
            lastMsgTime = Time.time;
        }

        void FixedUpdate() {
            float actTime = Time.time;
            if (actTime - lastMsgTime > msgDelta) {
                setMessage("");
            }
        }

        public void setFood(int foodInt) {
            food.text = "Food: " + foodInt;
        }

        public void setFinal(String msg) {
            final.text = msg;
        }

        public void setMessage(String message) {
            setMessage(message, 1f);
        }
        
        public void setMessage(String message, float delta) {
            messages.text = message;
            msgDelta = delta;
            lastMsgTime = Time.time;
        }
    }
}