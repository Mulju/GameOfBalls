using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject teleport1, teleport2, player;
    // Start is called before the first frame update
    void Start()
    {
        teleport1 = GameObject.FindGameObjectWithTag("Teleport1");
        teleport2 = GameObject.FindGameObjectWithTag("Teleport2");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Toimiva teleportti
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Teleport1"))
            {
                player.transform.position = new Vector2(teleport2.transform.position.x, teleport2.transform.position.y - teleport2.transform.localScale.y);
            }
            if (gameObject.CompareTag("Teleport2"))
            {
                player.transform.position = new Vector2(teleport1.transform.position.x, teleport1.transform.position.y - teleport1.transform.localScale.y);
            }
        }
    }
}
