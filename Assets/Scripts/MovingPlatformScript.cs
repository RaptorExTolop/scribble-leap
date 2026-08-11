using System;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {
    private Vector2 StartPos { get; set; }
    private Vector2 Range { get; set; }
    private float Facing { get; set; }
    private float Speed { get; set; }

    private void OnEnable() {
        StartPos = transform.position;
        Range = new (-4, 4);
        Facing = 1;
        Speed = 2f;
    }

    private void Update() {
        transform.position = new Vector3( transform.position.x + (Facing * Speed * Time.deltaTime), transform.position.y, transform.position.z);
        if (
            transform.position.x <= StartPos.x + Range.x ||
            transform.position.x >= StartPos.x + Range.y
        ) {
            Facing *= -1;
            Debug.Log("Swapping dir");
        }
        
    }
}