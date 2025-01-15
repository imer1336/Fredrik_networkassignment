using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class IceCubespawn : NetworkBehaviour
{
    public GameObject _asteroidPrefab;
    public float spawnTime = 2f;
    public float spawnRadius = 1f;

    private AsteroidMovement a_movement;

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        if (IsClient && !IsOwner) return;
        InvokeRepeating("SpawnIceCubeserverRpc", 0f, spawnTime);
    }

    [ServerRpc]
    public void SpawnIceCubeserverRpc()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        GameObject asteroid = Instantiate(_asteroidPrefab, spawnPosition, Quaternion.identity);
        asteroid.GetComponent<NetworkObject>().Spawn();
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Screen size
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Convert to world space
        Vector3 worldBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
        Vector3 worldTopRight = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, mainCamera.transform.position.z));

        Vector3 spawnPosition;

        // Random side
        if (Random.value < 0.5f) // Left/right
        {
            // Choose side
            float xPos = Random.value < 0.5f ? worldBottomLeft.x - spawnRadius : worldTopRight.x + spawnRadius;
            float yPos = Random.Range(worldBottomLeft.y, worldTopRight.y);
            spawnPosition = new Vector3(xPos, yPos, 0);
        }
        else // Top/bottom
        {
            float yPos = Random.value < 0.5f ? worldBottomLeft.y - spawnRadius : worldTopRight.y + spawnRadius;
            float xPos = Random.Range(worldBottomLeft.x, worldTopRight.x);
            spawnPosition = new Vector3(xPos, yPos, 0);
        }

        return spawnPosition; // Return position
    }

}
