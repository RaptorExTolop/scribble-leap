using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;


public class PlatformSpawner : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlatformScript[] Platforms;
    [SerializeField] public Camera GameCamera;
    [SerializeField] private int distFromCamBeforeRespawning;
    //[SerializeField] private int platformHeight = 32;

	public void SpawnPlatforms(int viewportOffset) {
        for (var i = 0; i < 20; ++i) {
            float randomX = UnityEngine.Random.Range(0.1f, 0.9f);
            float distFromCamera = Mathf.Abs(GameCamera.transform.position.z);
            Vector3 worldPoint = GameCamera.ViewportToWorldPoint(new Vector3(randomX, (float)(i*viewportOffset)/20, distFromCamera));
            spawnPlatform(new Vector2(worldPoint.x, worldPoint.y));
        }
	}

    public void spawnPlatform(Vector2 position) {
        var shouldSpawn = UnityEngine.Random.Range(0, 10);
        if (shouldSpawn < 10) {
            var random = UnityEngine.Random.Range(0, 12);
            if (random < 6) {
                GameObject newPlatform = Instantiate(Platforms[0].gameObject, new Vector3(position.x, position.y, 0), Quaternion.identity);
                gameManager.Platforms.Append(newPlatform.GetComponent<PlatformScript>());
            } else if (random < 7) {
                GameObject newPlatform = Instantiate(Platforms[1].gameObject, new Vector3(position.x, position.y, 0), Quaternion.identity);
                gameManager.Platforms.Append(newPlatform.GetComponent<PlatformScript>());
            } else {
                GameObject newPlatform = Instantiate(Platforms[2].gameObject, new Vector3(position.x, position.y, 0), Quaternion.identity);
                gameManager.Platforms.Append(newPlatform.GetComponent<PlatformScript>());
            }
        }
    }

	/*public void checkDeletePlatform(PlatformScript platform, Vector2 playerPosition) {
		if ()
	}*/
}

