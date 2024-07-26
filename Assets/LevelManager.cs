using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    private Camera mainCamera;
    private float camHeight;
    public float margin = 5f;

    void Start()
    {
        mainCamera = Camera.main;
        camHeight = 2f * mainCamera.orthographicSize;
    }

    void Update()
    {
        if (player.transform.position.y < mainCamera.transform.position.y - camHeight / 2f - margin)
        {
            Destroy(player);
            Debug.Log("Game Over");
        }
    }
}
