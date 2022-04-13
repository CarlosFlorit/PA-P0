using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //variables para velocidad del proyectil y la máxima distancia (para borrado)
    public float bulletSpeed;
    public float maxDistancia;

    private GameObject triggeringEnemy;
    public float damage;

  
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        maxDistancia += 1 * Time.deltaTime;

        if(maxDistancia >= 5)
        {
            //Destruye la bala para ahorrar recursos, ya que no necesitamos que recorra el espacio infinito
            Destroy(this.gameObject);
        }
    }

    //Si el proyectil choca con un enemigo le causa daño
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemigo")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().vidaEnemigo -= damage;
            Destroy(this.gameObject);
        }
    }



}
