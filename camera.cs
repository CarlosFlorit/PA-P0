using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Variables
    public Transform player;
    public float smooth = 0.04f;
    public float altura;

    private Vector3 velocity = Vector3.zero;



    //M�todos

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.y - 2f; // -2f es la rotaci�n de la c�mara para ver al jugador
        pos.y = player.position.z + altura;

        //suaviza el movimiento de la c�mara
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
      
    }
}
