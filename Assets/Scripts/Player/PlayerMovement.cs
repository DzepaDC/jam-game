using UnityEngine;

namespace Player {
    public class PlayerMovement : MonoBehaviour {
        
        private Rigidbody2D rb;
        private Vector2 moveVelocity;

        public float maxSpeed = 4f;

        public float backwardModifier = 0.7f;
        public float sideModifier = 0.85f;

        private Vector2 moveInput;
        
        void Start () {
            rb = GetComponent<Rigidbody2D>();
            
            
        }
        
        void Update () {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        
        private void FixedUpdate() {
            rb.velocity = moveInput.normalized * maxSpeed;
        }

        public Vector2 GetDirection() {
            return moveInput;
        }
    }
}