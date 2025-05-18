using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject plataformPrefab;
    private float distanciaEntrePlataformas = 5f;
    private float variacionAltura = 1.5f;
    private float maxAltura = 2.15f;              
    private float minAltura = -6.365156f;         
    private float minVerticalSeparation = 0.5f;  

    private Vector3 ultimaPosicion;
    public int contadorPlataformas = 3;
    private bool primeraEjecucion = true;

    private void Awake()
    {
   
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        if (contadorPlataformas < 3)
        {
            if (primeraEjecucion)
            {
                GameObject[] plataformas = GameObject.FindGameObjectsWithTag("Platform");
                float xMax = float.MinValue;
                foreach (GameObject plataforma in plataformas)
                {
                    if (plataforma.transform.position.x > xMax)
                    {
                        xMax = plataforma.transform.position.x;
                        ultimaPosicion = plataforma.transform.position;
                    }
                }
                primeraEjecucion = false;
            }

            InstanciarNuevaPlataforma();
        }
    }

    public void InstanciarNuevaPlataforma()
    {
        float nuevaY = ultimaPosicion.y;
        int intentos = 0;


        do
        {
            nuevaY = ultimaPosicion.y + Random.Range(-variacionAltura, variacionAltura);
            nuevaY = Mathf.Clamp(nuevaY, minAltura, maxAltura);
            intentos++;
        } while (Mathf.Abs(nuevaY - ultimaPosicion.y) < minVerticalSeparation && intentos < 10);

        Vector3 nuevaPos = new Vector3(ultimaPosicion.x + distanciaEntrePlataformas, nuevaY, 0);
        Instantiate(plataformPrefab, nuevaPos, Quaternion.identity);

        ultimaPosicion = nuevaPos;
        contadorPlataformas++;
    }

    public void PlataformaDestruida()
    {
        contadorPlataformas = Mathf.Max(contadorPlataformas - 1, 0);
    }
}
