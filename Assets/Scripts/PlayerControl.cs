using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float force;
    public float forceMultiplier = 10;
    public Rigidbody2D playerRB;

    public float health;
    public bool goingDown;
    public float damage;
    public float highPoint;

    public GameObject obstacleEffect;

    public Vector3 startPosition;

    public GUIStyle myStyle;

    // Start is called before the first frame update
    void Start()
    {
        myStyle.normal.textColor = Color.red;
        myStyle.fontSize = 16;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Time.deltaTime laskee kuinka kauan on mennyt sekunneissa edellisest� framesta
            // T�� tasottaa eri koneiden forcen kasvun samaksi, riippumata frameratesta
            force += forceMultiplier * Time.deltaTime;

            // Vaihdetaan pelaajan v�ri� kun ladataan palloa
            GetComponent<Renderer>().material.color = Color.white;
        }

        // N�� Vector3:set voi t�m�n pelin tapauksessa tod n�k teh� Vector2:na
        // En l�hde t�ss� kokeilemaan vaan kopioin open per�ss�
        if (Input.GetMouseButtonUp(0))
        {
            // Tallentaa hiiren paikan ruudulla muuttujaan
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Linjassa muiden z = 0 objektien kanssa

            // Katsotaan suunta ja normalisoidaan vektori, jolloin saadaan yhden yksik�n pituinen suuntavektori
            Vector3 dir = (mousePos - transform.position).normalized;

            // Jos hiiri on kartan alapuolella, pallo ammutaan hiirest� poisp�in
            if(dir.y < 0)
            {
                dir *= -1;
            }

            Launch(force, dir);
        }

        if(playerRB.velocity.y < -0.1 && goingDown == false)
        {
            // T�m� on se frame kun l�hdet��n ensimm�ist� kertaa alasp�in
            GetComponent<Renderer>().material.color = Color.red;
            goingDown = true;
            highPoint = gameObject.transform.position.y;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bar"))
        {

            if(goingDown == true && highPoint - gameObject.transform.position.y > 0.1)
            {
                // Ollaan hyp�tty edes v�h�n yl�sp�in ja ollaan tulossa alasp�in. Hyppy ollut yli 0.1 korkuinen.
                // Damage = nopeus jolla tullaan alas
                damage = Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y) * (highPoint - transform.position.y));
                TakeDamage(damage);
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(20);
            Instantiate(obstacleEffect, transform.position, Quaternion.identity);
        }
    }

    void TakeDamage(float dmg)
    {
        health -= dmg;
        if(health < 0)
        {
            Die();
        }
    }

    void Die()
    {
        transform.position = startPosition;
        playerRB.velocity = Vector3.zero;
        health = 30;
        goingDown = false;

        GameObject[] bars = GameObject.FindGameObjectsWithTag("Bar");
        foreach(GameObject bar in bars)
        {
            bar.layer = LayerMask.NameToLayer("BarInactive");
            bar.GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    void Launch(float launchForce, Vector3 launchDir)
    {
        goingDown= false;
        // Ammutaan pallo launchDir suuntaan launchForce:n voimalla
        playerRB.AddForce(launchDir * launchForce, ForceMode2D.Impulse);
        force = 0;
        // Vaihdetaan pelaajan v�ri� kun pallo l�htee yl�sp�in
        GetComponent<Renderer>().material.color = Color.magenta;
    }

    // Piirt�� n�yt�lle tietoa eri muuttujista
    private void OnGUI()
    {
        // Alla oleva komento piirt�� n�yt�lle tiettyyn kohtaan jotakin
        GUI.Label(new Rect(10, 10, 100, 20), "Force: " + force, myStyle);
        GUI.Label(new Rect(10, 30, 100, 20), "Health: " + health, myStyle);
        GUI.Label(new Rect(10, 50, 100, 20), "Damage: " + damage, myStyle);
        GUI.Label(new Rect(10, 70, 100, 20), "Going Down: " + goingDown, myStyle);
        GUI.Label(new Rect(10, 90, 100, 20), "High Point: " + highPoint, myStyle);
    }
}
