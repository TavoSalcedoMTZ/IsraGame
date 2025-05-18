using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 10f;
    public int jumpNumber = 1;
    private Animator animator;
    public TextMeshProUGUI textSaltos;
    public GameObject menuPanel; // Referencia al panel del menú

    private Camera mainCamera;
    private float camWidth;
    private float camHeight;
    private bool isMenuActive = false; // Estado del menú
    public GameManager gameManager; // Referencia al GameManager
    public UnityEvent onPlayerDamaged; 
    public UnityEvent jumpPlataform;

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
        if (collision.gameObject.CompareTag("Platform"))
        {
            rb.velocity = Vector2.up * (Random.Range(3f, 7f));
        jumpNumber++;
        StartCoroutine(JumpAnimation());

            jumpPlataform.Invoke();
           Destroy(collision.gameObject);
           gameManager.contadorPlataformas--;

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

    public void DañoAJugador()
    {
        if (jumpNumber > 0)
        {
            jumpNumber--;
        }
        onPlayerDamaged.Invoke();
        
    }
}
