using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    public float vidaEnemigo;
    public GameObject player;
    public float pointsToGive;


    //Métodos

    // Update is called once per frame
    void Update()
    {
        //mata enemigo si se queda sin puntos de vida
        if (vidaEnemigo <= 0)
        {
            EnemigoMuere();
        }
    }

    public void EnemigoMuere()
    {
        //destruye enemigo
        Destroy(this.gameObject);
        //da puntos al jugador
        player.GetComponent<Player>().points += pointsToGive;
    }


}
