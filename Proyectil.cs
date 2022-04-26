using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public bool rotate = false;
    public float rotateAmount = 45;
    public bool bounce = false;
    public float bounceForce = 10;
    public float speed;
    [Tooltip("From 0% to 100%")]
    public float accuracy;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public List<GameObject> trails;

    private Vector3 startPos;
    private float speedRandomness;
    private Vector3 offset;
    private bool collided;
    private Rigidbody rb;
    private RotateToMouseScript rotateToMouse;
    private GameObject target;

    public float damage;
    public float maxDistancia;

    private GameObject player;
    private GameObject triggeringEnemy;
    public Vector3 impactNormal;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        //player = GameObject.FindWithTag("Player");

        
        //Radio de precisión: cuando no es 100 las balas se dispersan. A veces no queda bien porque chocan contra el suelo
        if (accuracy != 100)
        {
            accuracy = 1 - (accuracy / 100);

            for (int i = 0; i < 2; i++)
            {
                var val = 1 * Random.Range(-accuracy, accuracy);
                var index = Random.Range(0, 2);
                if (i == 0)
                {
                    if (index == 0)
                        offset = new Vector3(0, -val, 0);
                    else
                        offset = new Vector3(0, val, 0);
                }
                else
                {
                    if (index == 0)
                        offset = new Vector3(0, offset.y, -val);
                    else
                        offset = new Vector3(0, offset.y, val);
                }
            }
        }

        //Crea el fogonazo si se ha añadido a la variable
        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward + offset;
            var ps = muzzleVFX.GetComponent<ParticleSystem>();
            if (ps != null)
                Destroy(muzzleVFX, ps.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
    }
    
    //He usado FixedUpdate en lugar de update
    void Update()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);
        /*
        maxDistancia += 1 * Time.deltaTime;

        if (maxDistancia >= 5)
        {
            Destroy(this.gameObject);
        }
        */
    }


    void FixedUpdate()
    {
        //"lanza" el proyectil hacia adelante
        rb.position += (transform.forward + offset) * (speed * Time.deltaTime);
        maxDistancia += 1 * Time.deltaTime;

        //Destruye el objeto para que no ralentice el juego cuando se crean muchos proyectiles
        if (maxDistancia >= 5)
        {
            Destroy(this.gameObject);
        }

    }







    public IEnumerator DestroyParticle(float waitTime)
    {

        if (transform.childCount > 0 && waitTime != 0)
        {
            List<Transform> tList = new List<Transform>();

            foreach (Transform t in transform.GetChild(0).transform)
            {
                tList.Add(t);
            }

            while (transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);
                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                for (int i = 0; i < tList.Count; i++)
                {
                    tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    //Colisión del proyectil
    public void OnTriggerEnter(Collider other)
    {
        //Si choca contra un enemigo
        if (other.tag == "Enemigo")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().vidaEnemigo -= damage; //quita vida al enemigo
            
            //instancia el efecto de impacto
            hitPrefab = Instantiate(hitPrefab, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            Destroy(this.gameObject); //elimina el proyectil

        }
        
        //si choca contra el jugador
        if (other.tag == "Player")
        {
            player.GetComponent<Player>().health -= 20; //reduce vida
            Destroy(this.gameObject); //elimina el proyectil
        }

        else
        {
            //si choca contra otra cosa (muro, suelo...) instancia el efecto de impacto y elimina el proyectil
            hitPrefab = Instantiate(hitPrefab, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            Destroy(this.gameObject);
        }

    }
}
