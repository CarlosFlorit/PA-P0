using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float movementSpeed;
    public GameObject camera;

    public GameObject playerObj;

    public GameObject bulletSpawnPoint;
    public float waitTime;
    public GameObject bala;

    private Transform bulletSpawned;

    public float points;

    //vida jugador
    public float maxHealth;
    public float health;

    public float currentTime;


    //salto
    public float jumpHeight = 50;
    public bool isGrounded;
    private Rigidbody rb;




    //Métodos
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {



        //El jugador mira hacia el puntero del ratón
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if(playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        //
        //Movimiento del jugador
        //

        //Avanzar
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        }

        //Izquierda
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

        }

        //Derecha
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        }

        //Atrás
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);

        }

        /*
        if (currentTime == 0 )
        {
            Disparo();
        }
        */

        if (currentTime < waitTime)
        {
            currentTime += 1 * Time.deltaTime;
        }

        if (currentTime >= waitTime)
        {
            currentTime = 0;
        }



        //
        //Disparo cañonaco
        //

        //if (Input.GetMouseButtonDown(0))
        if (Input.GetMouseButton(0) && currentTime == 0)
            {
            Disparo();
        }


        /*
        //muerte jugador
        if (health <= 0)
        {
            jugadorMuere();
        }

        */

        //salto
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.F))
            {
                rb.AddForce(Vector3.up * jumpHeight);
                print("Saltando");
            }
        }


    }



    void Disparo()
    {
        bulletSpawned = Instantiate(bala.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawned.rotation = bulletSpawnPoint.transform.rotation;
       
    }

    void jugadorMuere()
    {
        print("Estás frito");
    }


    //colisión al saltar
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            print("Toca suelo");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }



}
