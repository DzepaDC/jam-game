using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

namespace Player {
    public class PlayerState : MonoBehaviour{
        public int hungerLevel = 0;
        public int maxHungerLevel = 100;
    
        public int tempLevel = 0;
        public int maxTemp = 100;
        public int minTemp = -100;

        public Light2D eyes;
        public Light2D playerGlow;

        public bool eyesEnabled = false;
        
        
        void Start () {
            eyesEnabled = false;
            eyes.enabled = eyesEnabled;
            playerGlow.enabled = eyesEnabled;
        }

        public void enableEyes() {
            eyesEnabled = true;
            eyes.enabled = eyesEnabled;
            playerGlow.enabled = eyesEnabled;
        }
    }
}