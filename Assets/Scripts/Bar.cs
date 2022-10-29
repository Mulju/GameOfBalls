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
         * Yllä olevassa koodissa on se ongelma, että kun pelaaja osuu juuri tangon kohdalle, pelaajan modeli "hyppää" hieman ylöspäin ja näyttää tyhmältä
         * Siksi koodiin lisätään "gameObject.transform.localScale.y/2 + player.transform.localScale.y/2"
         * eli tangon puolikas paksuus ja pelaajan puolikas paksuus, jotta peli tunnistaa pelaajan ohittaneen tangon vasta kun pelaajan modeli
         * on oikeasti tangon yläpuolella
         */
        if (gameObject.transform.position.y + gameObject.transform.localScale.y/2 < player.transform.position.y)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
            gameObject.layer = LayerMask.NameToLayer("BarActive");
        }
    }
}
