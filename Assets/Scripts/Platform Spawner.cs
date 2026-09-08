using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class PlatformSpawner : MonoBehaviour {
    [SerializeField] private PlatformScript[] Platforms;
    [SerializeField] public Camera GameCamera;
    [SerializeField] private int distFromCamBeforeRespawning;

    [FormerlySerializedAs("platformHieght")] [SerializeField] private int platformHeight = 32;
    private int screenWidth = Screen.width;

    private void Awake() {
        for (var i = 0; i < 20; ++i) {
            float randomX = UnityEngine.Random.Range(0.1f, 0.9f);
            float distFromCamera = Mathf.Abs(GameCamera.transform.position.z);
            Vector3 worldPoint = GameCamera.ViewportToWorldPoint(new Vector3(randomX, (float)i/20, distFromCamera));
            //Debug.Log($"Hit: {didHit}. Hit point: {hit.point}. Randmon x: ${randomX}");
            spawnPlatform(new Vector2(worldPoint.x, worldPoint.y));
        }
    }

    private void spawnPlatform(Vector2 position) {
        Instantiate(Platforms[0].gameObject, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }

}
