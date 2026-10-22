using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float followOffset = 2f;
    public float smoothSpeed = 5f;

    private float highestY;

    private void Start()
    {
        highestY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        if (player.position.y > highestY - followOffset)
        {
            highestY = player.position.y + followOffset;
        }

        Vector3 targetPosition = new Vector3(transform.position.x, highestY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}