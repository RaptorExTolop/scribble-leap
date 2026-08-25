using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private int direction;
    public float speed = 1200;
    public float jumpHeight = 2000;
    private Rigidbody2D rb;
    public float maxSpeed = 800;

    private float jumpTimer = 0.100f;
    private float jumping = 0;
    private bool onPlatform;

    private void OnEnable() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        jumping = jumpTimer;
        onPlatform = false;
    }

    private void Update() {
        direction = 0;
        
        if (Input.GetKey(KeyCode.A)) {
            direction += -1;
        } 
        if (Input.GetKey(KeyCode.D)) {
            direction += 1;
        }

        if (onPlatform) {
            jumping -= Time.deltaTime;
            // Debug.Log(jumping);
            if (jumping < 0) {
                // Debug.Log("Jumping");
                rb.AddForce(new(0, jumpHeight));
                
                jumping = jumpTimer;
            }
        } else {
            jumping = jumpTimer;
        }
        
        rb.AddForce(new(speed * direction * Time.deltaTime, 0));
        float clampedX = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(clampedX, rb.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platforms")) {
            foreach (var contanct in other.contacts) {
                if (contanct.normal.y > 0.5f) {
                    onPlatform = true;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platforms")) {
            onPlatform = false;
        }
    }
}