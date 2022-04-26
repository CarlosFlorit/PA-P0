using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour
{
    private LineRenderer beam1;

    private Camera cam;

    private Vector3 origin;
    private Vector3 endPoint;
    private Vector3 mousePos;

    public Light beamLight;
    public Material beamMaterial;

    void Start()
    {
        // Crea laser
        
        beam1 = this.gameObject.AddComponent<LineRenderer>();
        beam1.startWidth = 0.1f;
        beam1.endWidth = 0.1f;
        beam1.material = beamMaterial;
        

        //beam1 = GetComponent<LineRenderer>();



        // coge la cámara principal
        cam = Camera.main;
    }


    void Update()
    {
        //el láser se apaga cuando no se pulsa el botón
        beamLight.enabled = false;

        //se enciende cuando se pulsa el botón
        if (Input.GetMouseButton(0))
        {
            checkLaser();
        }
        else beam1.enabled = false;
    }


    void checkLaser()
    {

        // Determina el origen del haz
        origin = this.transform.position + this.transform.forward * 0.5f * this.transform.lossyScale.z;


        // Encuentra el puntero del ratón
        mousePos = Input.mousePosition;
        mousePos.z = 300f;
        //endPoint = cam.ScreenToWorldPoint(mousePos);
        endPoint = transform.position + (transform.forward * 5000);


        

        // dirección del láser
        Vector3 dir = endPoint - origin;
        dir.Normalize();

        // Choca con objeto
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, 300f))
        {
            //final del láser en el objeto (para que no lo atraviese)
            endPoint = hit.point;
            beamLight.transform.position = hit.point;

            //Ilumina el objeto
            beamLight.enabled = true;

            /*
            // Has this hit object got a rigidbody? 
            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.transform.GetComponent<Rigidbody>().
                    AddForce(dir, ForceMode.Impulse);
            }
            */
        }

        beam1.SetPosition(0, origin); //origen del láser
        beam1.SetPosition(1, endPoint); //final del láser
        // Enciende el láser
        beam1.enabled = true;

    }
}
