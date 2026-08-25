using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakingPlatformScript : PlatformScript {
    [SerializeField] public GameObject[] platforms = new GameObject[3];

    // breaking
    private bool playerStanding = false;
    private float BreakTimer = 0.5f;
    private float timeUntilNextBreakStep = 0.0f;
    private int breakStep = 0;
    private Vector2 ExplodeForceRange { get; set; }
    
    // shaking
    public float shakeStrength = 75f;
    private float ShakeTimer = 0.2f;
    private float shakeing = 0;
    public float shakeAmount = 0.2f;
    private Vector2 startPos { get; set; }

    private void OnEnable() {
        timeUntilNextBreakStep = BreakTimer;
        breakStep = 0;
        ExplodeForceRange = new Vector2(-200, 200);
        startPos = transform.position;
        shakeing = -1;
    }

    private void Update() {
        shakeing -= Time.deltaTime;    
        /* if thingy is thingy shaking*/
        if (shakeing > 0) {
            Vector2 chasePosition = startPos + (Random.insideUnitCircle.normalized * shakeAmount);
            gameObject.transform.position =
                Vector3.Lerp(transform.position, chasePosition, Time.deltaTime * shakeStrength);
        }
        /* if we are not shaking go back towards the start position */
        else if (breakStep < 3) {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * shakeStrength);
        }

        if (playerStanding) {
            /* update timers */
            timeUntilNextBreakStep -= Time.deltaTime;
            if (timeUntilNextBreakStep <= 0) {
                breakStep++;
                timeUntilNextBreakStep = BreakTimer;
                if (breakStep < 3) {
                    shakeing = ShakeTimer;
                }
                else if (breakStep == 3) {
                    Break();
                }
                else if (breakStep > 5) {
                    Destroy(gameObject);
                }
            }
        }
        else {
            timeUntilNextBreakStep = BreakTimer;
        }
    }

    private void Break() {
            GetComponent<BoxCollider2D>().isTrigger = true;
            foreach (var piece in platforms) {
                var rb = piece.GetComponent<Rigidbody2D>();
                rb.simulated = true;
                rb.AddForce(
                    new Vector2(
                        UnityEngine.Random.Range(ExplodeForceRange.x, ExplodeForceRange.y),
                        UnityEngine.Random.Range(ExplodeForceRange.x, ExplodeForceRange.y)
                    )
                );

            }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerStanding = true;
        }
    }

    /*private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerStanding = false;
        }
    }*/
}
