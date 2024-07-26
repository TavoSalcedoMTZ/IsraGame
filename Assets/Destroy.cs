using System.Collections;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.name.StartsWith("Platform"))
        {

            if (Random.Range(1, 7) == 1)
            {

                Destroy(collision.gameObject);
                Instantiate(springPrefab, new Vector2(Random.Range(-8f, 8f), player.transform.position.y + (14 + Random.Range(0.5f, 1.0f))), Quaternion.identity);


            } else
            {

                collision.gameObject.transform.position = new Vector2(Random.Range(-8f, 8f), player.transform.position.y + (14 + Random.Range(0.5f, 1.0f)));

            }

        } else if(collision.gameObject.name.StartsWith("Spring"))
        {

            if (Random.Range(1, 7) == 1)
            {

                collision.gameObject.transform.position = new Vector2(Random.Range(-8f, 8f), player.transform.position.y + (14 + Random.Range(0.5f, 1.0f)));

            }
            else
            {
                int randomNumber = Random.Range(1, 8);
                Destroy(collision.gameObject);
                if (randomNumber == 1)
                {
                    Instantiate(platformPrefabf, new Vector2(Random.Range(-8f, 8f), player.transform.position.y + (14 + Random.Range(0.5f, 1.0f))), Quaternion.identity);
                }else if (randomNumber == 2)
                {
                    Instantiate(platformPrefabf1, new Vector2(Random.Range(-8f, 8f), player.transform.position.y + (14 + Random.Range(0.5f, 1.0f))), Quaternion.identity);
                }else if(randomNumber == 3)
                {
                    Instantiate(platformPrefabf2, new Vector2(Random.Range(-8f, 8f), player.transform.position.y + (14 + Random.Range(0.5f, 1.0f))), Quaternion.identity);
                }
                else
                {
                    Instantiate(platformPrefab, new Vector2(Random.Range(-8f, 8f), player.transform.position.y + (14 + Random.Range(0.5f, 1.0f))), Quaternion.identity);
                }
                

            }

        }
    }

}
