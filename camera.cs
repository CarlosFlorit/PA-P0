using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Variables
    public Transform player;
    public float smooth = 0.04f; 
    public float altura; //para establecer la altura de la cámara

    private Vector3 velocity = Vector3.zero;



    //Métodos

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.y - 2f; // -2f es la rotación de la cámara para ver al jugador
        pos.y = player.position.z + altura;

        //suaviza el movimiento de la cámara
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
      
    }
}
