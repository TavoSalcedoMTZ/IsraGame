using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Player player;
    public float speed = 2f;
    public void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

    }
    private void Start()
    {
        player= FindFirstObjectByType<Player>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("El enemigo ha colisionado con el jugador");
            player.DañoAJugador();
            StartCoroutine(Destructor());
        }
     }

    private IEnumerator Destructor()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}

