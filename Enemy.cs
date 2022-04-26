using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    public float vidaEnemigo;
    public GameObject player;
    public float pointsToGive;

    public float waitTime;
    public float currentTime;
    private bool shot;

    public GameObject bullet;
    public GameObject enemyBulletSpawnPoint;
    public Transform bulletSpawned;


    public float stoppingDistance;
    public float speed;

    public bool esSuicida;




    //Methods

    public void Start()
    {
        //player = GameObject.FindWithTag("Player");
        


        //bulletSpawnPoint = GameObject.Find("Cannon_Holder/bullet_spawn_point");

    }


    // Update is called once per frame
    void Update()
    {
        if (vidaEnemigo <= 0)
        {
            EnemigoMuere();
        }

        //Enemigo se encara hacia el jugador
        this.transform.LookAt(player.transform);


        if (currentTime == 0 && esSuicida == false)
        {
            EnemigoDispara();
        }

        if (shot && currentTime < waitTime)
        {
            currentTime += 1 * Time.deltaTime;
        }

        if (currentTime >= waitTime)
        {
            currentTime = 0;
        }


        //enemigo se mueve hacia el jugador y se detiene a distancia
        if (Vector3.Distance(transform.position, player.transform.position) > stoppingDistance && esSuicida == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }





        //si el enemigo es suicida se lanza contra el jugador
        else if (esSuicida == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }





    }
    //fin de Update

        public void EnemigoMuere()
    {
        Destroy(this.gameObject);
        player.GetComponent<Player>().points += pointsToGive;
    }

    public void EnemigoDispara()
    {
        shot = true;
        //print("disparando");

        bulletSpawned = Instantiate(bullet.transform, enemyBulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawned.rotation = this.transform.rotation;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (esSuicida == true && other.tag == "Player")
        {
            player.GetComponent<Player>().health -= 20;
            Destroy(this.gameObject);
        }
    }



}
