using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private int direction;
    private float speed = 800;
    private float jumpHeight = 690;
    private Rigidbody2D rb;

    private void OnEnable() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        direction = 0;
        
        if (Input.GetKey(KeyCode.A)) {
            direction += -1;
        } 
        if (Input.GetKey(KeyCode.D)) {
            direction += 1;
        }

        /*if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) {
            rb.AddForce(new Vector2(0, jumpHeight * Time.deltaTime));
        }*/
        
        rb.AddForce(new Vector2(direction * speed * Time.deltaTime, 0));
    }
}
