using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    private float fixedHeight = -1.86f; // Altura fija de la c�mara

    void Start()
    {
        if (player == null)
        {
            // Buscar el jugador por el nombre del objeto si no est� asignado
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Mantener fija la altura de la c�mara en el valor de fixedHeight
            transform.position = new Vector3(player.position.x, fixedHeight, transform.position.z);
        }
    }
}
