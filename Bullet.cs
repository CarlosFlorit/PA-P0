using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float maxDistancia;

    private GameObject triggeringEnemy;
    public float damage;

    private GameObject player;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }




    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        maxDistancia += 1 * Time.deltaTime;

        if(maxDistancia >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemigo")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().vidaEnemigo -= damage;
            Destroy(this.gameObject);
        }

        if (other.tag == "Player")
        {
            player.GetComponent<Player>().health -= 20;
            Destroy(this.gameObject);
        }

    }



}
