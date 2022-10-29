using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /* gameObject.transform.position.y < player.transform.position.y
         * Yll� olevassa koodissa on se ongelma, ett� kun pelaaja osuu juuri tangon kohdalle, pelaajan modeli "hypp��" hieman yl�sp�in ja n�ytt�� tyhm�lt�
         * Siksi koodiin lis�t��n "gameObject.transform.localScale.y/2 + player.transform.localScale.y/2"
         * eli tangon puolikas paksuus ja pelaajan puolikas paksuus, jotta peli tunnistaa pelaajan ohittaneen tangon vasta kun pelaajan modeli
         * on oikeasti tangon yl�puolella
         */
        if (gameObject.transform.position.y + gameObject.transform.localScale.y/2 < player.transform.position.y)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
            gameObject.layer = LayerMask.NameToLayer("BarActive");
        }
    }
}
