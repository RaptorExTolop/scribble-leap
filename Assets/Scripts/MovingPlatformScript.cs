using System;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {
    private Vector2 StartPos { get; set; }
    [SerializeField] private Vector2 Range;
    private float Facing { get; set; }
    [SerializeField] private float Speed ;

    private void OnEnable() {
        StartPos = transform.position;
        Facing = 1;
    }

    private void Update() {
        transform.position = new Vector3( transform.position.x + (Facing * Speed * Time.deltaTime), transform.position.y, transform.position.z);
        if (
            transform.position.x <= StartPos.x + Range.x ||
            transform.position.x >= StartPos.x + Range.y
        ) {
            Facing *= -1;
        }
        
    }
}