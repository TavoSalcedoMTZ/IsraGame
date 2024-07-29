using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    private Camera mainCamera;
    private float camHeight;
    public float margin = 5f;
    public string gameOverSceneName = "GameOverScene"; 

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
            SceneLoad();
        }
    }

    void SceneLoad()
    {
        SceneManager.LoadScene(gameOverSceneName); 
    }
}
