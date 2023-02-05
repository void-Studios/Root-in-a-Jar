using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject PlatformHolder;
    
    public float spawnInterval = 2f;
    private Camera mainCamera;
    private float playerPosY;
    private float spawnPosY = 0f;
    public float spawnVariance = 2.5f;
    private float cameraHalfWidth;

    void Start()
    {
        
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.aspect * mainCamera.orthographicSize;
        
        
        InvokeRepeating(nameof(SpawnPlatform), 0f, spawnInterval);
    }
    public float GetPlayerPosY()
    {
        playerPosY = GameObject.FindObjectOfType<Player>().transform.position.y;
        int aproxValue = Random.Range(0, 2);
        if (aproxValue == 0)
        {
            playerPosY = (int)playerPosY + .5f;
        }
        else
        {
            playerPosY = (int)playerPosY - .5f;
        }
        return playerPosY;
    }
    private void SpawnPlatform()
    {
        
        if (PlatformHolder.transform.childCount > 15)
        {
            return;
        }
        for (int i = 0; i < 3; i++)
        {
            float randomX = Random.Range(-cameraHalfWidth, cameraHalfWidth);
            Vector3 spawnPos = new Vector3(randomX, spawnPosY, 0f);
            Instantiate(platformPrefab, spawnPos, Quaternion.identity,PlatformHolder.transform);

            spawnPosY = GetPlayerPosY() + 2.5f;
        }

        
    }
}
