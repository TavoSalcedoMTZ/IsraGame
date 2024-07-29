using UnityEngine;
using TMPro;

public class DistanceCounter : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    private Vector3 startPosition;
    private float distanceTravelled;

    void Start()
    {
        startPosition = transform.position;
        distanceText.gameObject.SetActive(false);
    }

    void Update()
    {
        distanceTravelled = transform.position.x - startPosition.x;

        if (distanceTravelled > 0)
        {
            distanceText.gameObject.SetActive(true);
            distanceText.text = distanceTravelled.ToString("F1");
        }
        else
        {
            distanceText.gameObject.SetActive(false);
        }
    }
}
