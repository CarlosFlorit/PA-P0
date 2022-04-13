using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour
{
    //Creamos line renderer para el puntero láser
    private LineRenderer beam1;

    private Camera cam;

    private Vector3 origin;
    private Vector3 endPoint;
    private Vector3 mousePos;

    public Light beamLight;

    void Start()
    {
        //Cogemos el láser del line renderer y le asignamos grosor (finito)
        beam1 = this.gameObject.AddComponent<LineRenderer>();
        beam1.startWidth = 0.1f;
        beam1.endWidth = 0.1f;

        

        // Grab the main camera.
        cam = Camera.main;
    }


    void Update()
    {
        //Se apaga si no pulsamos el botón
        beamLight.enabled = false;

        //Se enciende cuando apretamos el botón derecho del ratón
        if (Input.GetMouseButton(0))
        {
            checkLaser();
        }
        else beam1.enabled = false;
    }


    void checkLaser()
    {

        // Origen y final del láser
        origin = this.transform.position + this.transform.forward * 0.5f * this.transform.lossyScale.z;


        // Encuentra el puntero en el mundo 3D
        mousePos = Input.mousePosition;
        mousePos.z = 300f;
        //endPoint = cam.ScreenToWorldPoint(mousePos);
        endPoint = transform.position + (transform.forward * 5000);


        

        // Dirección del haz
        Vector3 dir = endPoint - origin;
        dir.Normalize();

        // ¿Choca con un objeto?
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, 300f))
        {
            // Si choca, determinamos el final sobre el objeto
            endPoint = hit.point;
            beamLight.transform.position = hit.point;

            //beamLight.enabled = !beamLight.enabled;
            beamLight.enabled = true;

            /*
            // Aplica fuerza al objeto con rigid body (no lo voy a usar, sólo quiero usar el láser para facilitar el apuntado) 
            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.transform.GetComponent<Rigidbody>().
                    AddForce(dir, ForceMode.Impulse);
            }
            */
        }

        // Final del láser
        beam1.SetPosition(0, origin);
        beam1.SetPosition(1, endPoint);
        // Activamos el láser
        beam1.enabled = true;

    }
}
