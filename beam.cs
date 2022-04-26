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



        // Grab the main camera.
        cam = Camera.main;
    }


    void Update()
    {
        beamLight.enabled = false;

        if (Input.GetMouseButton(0))
        {
            checkLaser();
        }
        else beam1.enabled = false;
    }


    void checkLaser()
    {

        // Finding the origin and end point of laser.
        origin = this.transform.position + this.transform.forward * 0.5f * this.transform.lossyScale.z;


        // Finding mouse pos in 3D space.
        mousePos = Input.mousePosition;
        mousePos.z = 300f;
        //endPoint = cam.ScreenToWorldPoint(mousePos);
        endPoint = transform.position + (transform.forward * 5000);


        

        // Find direction of beam.
        Vector3 dir = endPoint - origin;
        dir.Normalize();

        // Are we hitting any colliders?
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, 300f))
        {
            // If yes, then set endpoint to hit-point.
            endPoint = hit.point;
            beamLight.transform.position = hit.point;

            //beamLight.enabled = !beamLight.enabled;
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

        // Set end point of laser.
        beam1.SetPosition(0, origin);
        beam1.SetPosition(1, endPoint);
        // Draw the laser!
        beam1.enabled = true;

    }
}
