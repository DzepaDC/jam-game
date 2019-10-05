using System;
using UnityEngine;

namespace Player {
    public class PlayerInteraction : MonoBehaviour {
        public bool haveArms;
        public bool canEat;

        private void Start() {
            haveArms = false;
            canEat = false;
        }

        private void Update() {
            throw new NotImplementedException();
        }
        
        
    }
}