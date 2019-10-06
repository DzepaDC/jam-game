using UnityEngine;

namespace Player {
    public class PlayerMovement : MonoBehaviour {
        
        private Rigidbody2D rb;
        private Vector2 moveVelocity;
        private Vector2 moveInput;

        public float maxSpeed = 4f;

        public float backwardModifier = 0.7f;
        public float sideModifier = 0.85f;

        void Start () {
            rb = GetComponent<Rigidbody2D>();
        }
        
        void Update () {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * maxSpeed;
        }
        
        private void FixedUpdate() {
            rb.velocity = moveVelocity;
        }

        public Vector2 GetDirection() {
            return moveInput;
        }

        public bool isMoving() {
            return moveInput != Vector2.zero;
        }

        public void hardStop() {
            rb.velocity = Vector2.zero;
            moveInput = Vector2.zero;
        }
    }
}