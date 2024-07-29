using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 10f;
    public int jumpNumber = 1;
    private Animator animator;
    public TextMeshProUGUI textSaltos;
    public GameObject platformPrefab;
    public GameObject menuPanel; // Referencia al panel del menú

    private Camera mainCamera;
    private float camWidth;
    private float camHeight;
    private bool isMenuActive = false; // Estado del menú

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        mainCamera = Camera.main;
        camHeight = 2f * mainCamera.orthographicSize;
        camWidth = camHeight * mainCamera.aspect;

        menuPanel.SetActive(false); // Asegúrate de que el menú esté oculto al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        if (isMenuActive) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.position += movement * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && jumpNumber > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpNumber--;
            StartCoroutine(JumpAnimation());
        }
        textSaltos.text = jumpNumber.ToString();
        transform.rotation = Quaternion.identity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.up * (Random.Range(3f, 7f));
        jumpNumber++;
        StartCoroutine(JumpAnimation());

        if (collision.gameObject.name.StartsWith("Platform"))
        {
            platformPrefab = collision.gameObject;

            float spawnX = Random.Range(transform.position.x + 5f, mainCamera.transform.position.x + camWidth / 2f + 10f);
            float spawnY = Random.Range(mainCamera.transform.position.y - camHeight / 2f, mainCamera.transform.position.y + camHeight / 2f);
            Vector2 newPosition = new Vector2(spawnX, spawnY);

            GameObject newPlatform = Instantiate(platformPrefab, newPosition, Quaternion.identity);

            BoxCollider2D collider = newPlatform.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                collider = newPlatform.AddComponent<BoxCollider2D>();
            }
            collider.enabled = true;

            Destroy(collision.gameObject);
        }
    }

    private IEnumerator JumpAnimation()
    {
        animator.SetBool("isJumping", true);
        yield return new WaitForSeconds(0.24f);
        animator.SetBool("isJumping", false);
    }

    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        menuPanel.SetActive(isMenuActive);
        Time.timeScale = isMenuActive ? 0 : 1; 
    }
}
