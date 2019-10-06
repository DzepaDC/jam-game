using System;
using Player;
using UnityEngine;

namespace Interactive {
    public class DoorHor : MonoBehaviour, ITrigger {
        public GameObject doorOpened;
        public GameObject doorClosed;
        
        public bool isOpen = false;

        private void Start() {
            if (!isOpen) {
                doorOpened.GetComponent<SpriteRenderer>().enabled = false;
                doorClosed.GetComponent<SpriteRenderer>().enabled = true;
                doorClosed.GetComponent<BoxCollider2D>().enabled = true;
            } else {
                doorOpened.GetComponent<SpriteRenderer>().enabled = true;
                doorClosed.GetComponent<SpriteRenderer>().enabled = false;
                doorClosed.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        public void open() {
            isOpen = true;
            
            doorOpened.GetComponent<SpriteRenderer>().enabled = true;
            doorClosed.GetComponent<SpriteRenderer>().enabled = false;
            doorClosed.GetComponent<BoxCollider2D>().enabled = false;
        }

        public void close() {
            isOpen = false;
            
            doorOpened.GetComponent<SpriteRenderer>().enabled = false;
            doorClosed.GetComponent<SpriteRenderer>().enabled = true;
            doorClosed.GetComponent<BoxCollider2D>().enabled = true;
        }

        public void execute() {
            if (isOpen) {
                close();
            } else {
                open();
            }
        }
    }
}