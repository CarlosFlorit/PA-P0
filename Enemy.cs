using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    public float vidaEnemigo;
    public GameObject player;
    public float pointsToGive;


    //Methods

    // Update is called once per frame
    void Update()
    {
        if (vidaEnemigo <= 0)
        {
            EnemigoMuere();
        }
    }

    public void EnemigoMuere()
    {
        Destroy(this.gameObject);
        player.GetComponent<Player>().points += pointsToGive;
    }


}
