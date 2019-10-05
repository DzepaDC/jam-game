using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class EyeItem : MonoBehaviour
{
    public bool isRight = true;
    
    public GameObject moveable;
    private Vector3 initVect;

    void Start() {
        initVect = moveable.transform.position;
    }
        
    void FixedUpdate() {
        moveable.transform.position = initVect + new Vector3(0, (float) Math.Sin(Time.time * 9f) * 0.1f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerState player = collision.GetComponent<PlayerState>();
        if (player != null) {
            player.enableEyes(isRight);
            Destroy(gameObject);
        }
    }
}
