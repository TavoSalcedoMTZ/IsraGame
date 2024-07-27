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
    public string gameOverSceneName = "GameOverScene"; // Nombre de la escena de Game Over

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
            SceneLoad();
        }
    }

    void SceneLoad()
    {
        SceneManager.LoadScene(gameOverSceneName); 
    }
}
