using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{
    public Text text;
    public GameObject player;
    public GameObject platformPrefab;
    public GameObject springPrefab;

    public GameObject platformPrefabf;
    public GameObject platformPrefabf1;
    public GameObject platformPrefabf2;

    public float minDistance = 4f; // Distancia mínima entre plataformas

    void Start()
    {

    }

    void Update()
    {

    }

    private bool IsPositionTooClose(Vector2 newPos)
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject platform in platforms)
        {
            if (Mathf.Abs(platform.transform.position.y - newPos.y) < minDistance)
            {
                return true; // Está muy cerca de otra plataforma
            }
        }
        return false;
    }

    private Vector2 GetSafePosition()
    {
        Vector2 newPosition;
        int attempts = 0;
        do
        {
            newPosition = new Vector2(Random.Range(-8f, 8f), player.transform.position.y + (14 + Random.Range(0.5f, 1.0f)));
            attempts++;
        } while (IsPositionTooClose(newPosition) && attempts < 10);

        return newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Platform"))
        {
            if (Random.Range(1, 7) == 1)
            {
                Destroy(collision.gameObject);
                Vector2 spawnPos = GetSafePosition();
                Instantiate(springPrefab, spawnPos, Quaternion.identity);
            }
            else
            {
                Vector2 newPosition = GetSafePosition();
                collision.gameObject.transform.position = newPosition;
            }
        }
        else if (collision.gameObject.name.StartsWith("Spring"))
        {
            if (Random.Range(1, 7) == 1)
            {
                Vector2 newPosition = GetSafePosition();
                collision.gameObject.transform.position = newPosition;
            }
            else
            {
                Destroy(collision.gameObject);

                int randomNumber = Random.Range(1, 8);
                Vector2 spawnPos = GetSafePosition();

                if (randomNumber == 1)
                {
                    Instantiate(platformPrefabf, spawnPos, Quaternion.identity);
                }
                else if (randomNumber == 2)
                {
                    Instantiate(platformPrefabf1, spawnPos, Quaternion.identity);
                }
                else if (randomNumber == 3)
                {
                    Instantiate(platformPrefabf2, spawnPos, Quaternion.identity);
                }
                else
                {
                    Instantiate(platformPrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }
}
