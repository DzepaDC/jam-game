using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    private Vector2 velocity;

    public float smoothTimeX;
    public float smoothTimeY;

    public GameObject target;

    void Start() {
    }

    void FixedUpdate() {
        // smooth cam movement
        float posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

        // sharp movement
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
