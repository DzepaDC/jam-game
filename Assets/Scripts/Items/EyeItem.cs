using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class EyeItem : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerState player = collision.GetComponent<PlayerState>();
        if (player != null) {
            player.enableEyes();
        }
    }
}
