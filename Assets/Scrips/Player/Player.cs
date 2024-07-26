using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 10f;
    public int jumpNumber = 1;

    // Añadir referencia al prefab de la plataforma
    public GameObject platformPrefab;

    private Camera mainCamera;
    private float camWidth;
    private float camHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Obtener la cámara principal y sus dimensiones
        mainCamera = Camera.main;
        camHeight = 2f * mainCamera.orthographicSize;
        camWidth = camHeight * mainCamera.aspect;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.position += movement * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && jumpNumber > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpNumber--;
        }

        // Fijar la rotación del jugador en 0, 0, 0
        transform.rotation = Quaternion.identity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.up * (Random.Range(3f, 7f));
        jumpNumber++;

        // Comprobar si el objeto con el que colisiona es una plataforma
        if (collision.gameObject.name.StartsWith("Platform"))
        {
            // Referencia al prefab de la plataforma desde el objeto colisionado
            platformPrefab = collision.gameObject;

            // Generar nueva plataforma dentro de los límites de la cámara y a la derecha del jugador
            float spawnX = Random.Range(transform.position.x + 5f, mainCamera.transform.position.x + camWidth / 2f + 10f);
            float spawnY = Random.Range(mainCamera.transform.position.y - camHeight / 2f, mainCamera.transform.position.y + camHeight / 2f);
            Vector2 newPosition = new Vector2(spawnX, spawnY);

            GameObject newPlatform = Instantiate(platformPrefab, newPosition, Quaternion.identity);

            // Asegurar que la nueva plataforma tenga un BoxCollider2D activado
            BoxCollider2D collider = newPlatform.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                collider = newPlatform.AddComponent<BoxCollider2D>();
            }
            collider.enabled = true; // Activar el BoxCollider2D

            // Destruir la plataforma original
            Destroy(collision.gameObject);
        }
    }
}
