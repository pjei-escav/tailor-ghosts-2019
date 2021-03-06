﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SonidoFantasmas))]
//Obligamos al objeto que tiene el script a llevar el script de sonidoFantasmas
public class Fantasma_Baboso: MonoBehaviour
{

    SonidoFantasmas sonido;

    public GameObject fantasmaNormal;
    Rigidbody2D rb;
    GameObject gc;

    //Variables de los Controles
    Vector3 pincho;
    Vector3 suelto;
    public float velocidad = 10f;
    public bool puedoMoverme = true;
    //Creamos variables bool para saber la direccion del baboso
    public static bool baja = false;
    public static bool sube = false;
    public static bool izda = false;
    public static bool dcha = false;

    private void Awake()
    {
        //Comprobamos que tenga el GameController
        gc = GameObject.Find("GameController");
        if(gc == null)
        {
            Debug.LogError("No encuntro el GameController");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Reposiciona();  
        rb = GetComponent<Rigidbody2D>();
        sonido = GetComponent<SonidoFantasmas>();

    }
    // Update is called once per frame
    void Update()
    {


    }
    void OnMouseDown()
    {
        pincho = Input.mousePosition;
    }
    //Cuando suelto obtengo la posición donde he soltado y la guardo en el vector suelto
    void OnMouseUp()
    {
        suelto = Input.mousePosition;
        Mover();

    }
    void Mover()
    {
        Vector3 dif = suelto - pincho;
        if (puedoMoverme)
        {
            if (Mathf.Abs(dif.x) > Mathf.Abs(dif.y))
            {
                if (dif.x > 0)
                {
                    rb.velocity = new Vector2(1, 0) * velocidad;
                    //En cada movimiento activamos la direccion que lleve el baboso
                    dcha = true;
                }
                else
                {
                    rb.velocity = new Vector2(-1, 0) * velocidad;
                    izda = true;
                }

                puedoMoverme = false;
            }
            else
            {
                if (dif.y > 0)
                {
                    rb.velocity = new Vector2(0, 1) * velocidad;
                    sube = true;
                }
                else
                {
                    rb.velocity = new Vector2(0, -1) * velocidad;
                    baja = true;
                }

                puedoMoverme = false;
            }

        }
        //Reproduce el sonido de mover
        sonido.SonidoMover();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //Recuadra el sprite
        Reposiciona();
        if (col.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (!col.transform.parent == transform)
            {
                //Estas lineas de codigo la comparten todos los fantasmas, crea un normal cuando chocan
                GameObject nuevoFantasma;
                nuevoFantasma = Instantiate(fantasmaNormal, transform.position, transform.rotation);               
                nuevoFantasma.transform.SetParent(col.transform);
                nuevoFantasma.GetComponent<Cremallera>().Comprobador();
                Destroy(nuevoFantasma.GetComponent<Rigidbody2D>());

                //Debug.Log("hijobasoso");
                gc.GetComponent<GameController_ingame>().RestarFantasmas();
                Destroy(gameObject);
            }

           
        }
        else
        {
            puedoMoverme = true;

        }
        if(rb.velocity == Vector2.zero)
        {
            //Cuando el baboso está parado, no va en ninguna direccion 
            baja = false;
            sube = false;
            izda = false;
            dcha = false;
        }


    }
    public void Reposiciona()
    {
        if (transform.parent != null)
        {

            transform.localPosition = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), Mathf.Round(transform.localPosition.z));
        }
        else
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        }

    }
}
