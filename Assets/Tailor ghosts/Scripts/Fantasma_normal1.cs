﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SonidoFantasmas))]
public class Fantasma_normal1 : MonoBehaviour
{
    public GameObject gc;

    //Creamos dos vectores, uno para detectar dónde pulso por primera vez
    //y dónde suelto para más tarde calcular la diferencia entre esas posiciones

    Vector3 pincho;
    Vector3 suelto;
    public Vector2 direccion;

    //Declaramos una velocidad 
    //Y un booleano para saber si podemos movernos o no para así impedir
    //que el jugador pueda mover el fantasma mientras este realiza su trayectoria

    public float velocidad = 10f;
    public bool puedoMoverme = true;
    Fantasma_normal1 scriptFantasma;

    //Declaramos el rb del fantasma
    Rigidbody2D rb;

    SonidoFantasmas sonido;

    private void Awake()
    {
        gc = GameObject.Find("GameController");
    }

    void Start()
    {
        //Obtenemos el rb de el fantasma
        rb = GetComponent<Rigidbody2D>();
        scriptFantasma = GetComponent<Fantasma_normal1>();
        Reposiciona();
        sonido = GetComponent<SonidoFantasmas>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Funcion para poder moverme
     void Mover()
     {
        //Declaramos un vector que obtenga la diferencia 
        //Entre la posición donde pinchamos y dónde soltamos
        //para saber la dirección en la que irá nuestro personaje

         Vector3 dif = suelto - pincho;

         if (puedoMoverme)
         {

            //Si el valor absoluto en x es mayor que el valor absoluto en y
            //Entonces la velocidad irá en el eje x
            //dependiendo de si la dirección es positiva o negativa irá hacia arriba o hacia abajo
             if (Mathf.Abs(dif.x) > Mathf.Abs(dif.y))
             {
                 if(dif.x>0)
                 {
                    rb.velocity = new Vector2(1, 0) * velocidad;
                    direccion = rb.velocity;

                 }
                 else
                 {
                    rb.velocity = new Vector2(-1, 0) * velocidad;
                    direccion = rb.velocity;
                 }

                 puedoMoverme = false;
             }
            //Si no, la velocidad irá en el eje y
            //dependiendo de si la dirección es positiva o negativa irá hacia derecha o izquierda
            else
            {
                 if (dif.y>0)
                 {
                    rb.velocity = new Vector2(0, 1) * velocidad;
                    direccion = rb.velocity;
                 }
                 else
                 {
                    rb.velocity = new Vector2(0, -1) * velocidad;
                    direccion = rb.velocity;
                 }

                 puedoMoverme = false;
             }

            sonido.SonidoMover();
        }




     }

    //Cuando pincho obtengo la posición donde he pinchado y la guardo en el vector pincho
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


    //Cuando choco con un colider puedo volver a moverme
    //Si este collider es de tipo Player lo convierto en mi hijo si no lo es en ese instante
    //le destruyo a mi hijo el rb y el script para que sea un sprite 
    //resto del numero de fantasmas en pantalla y puedo volver a moverme


    private void OnCollisionEnter2D(Collision2D col)
    {
        Reposiciona();
        
        puedoMoverme = true;
        if (col.gameObject.CompareTag("Player"))
        {
            
            

            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
            
            
            if (! col.transform.parent == transform && transform.CompareTag("Player"))
            {
                gc.GetComponent<GameController_ingame>().RestarFantasmas();
                transform.SetParent(col.transform);
                if(transform.parent != null)
                {
                    Destroy(rb);
                }
                Reposiciona();
                
                Destroy(scriptFantasma);
                Destroy(rb);                                           
            }

            

        }


       
    }

    //Si choco con un collider puedo volver a moverme

    private void OnTriggerEnter2D(Collider2D col)
    {
        puedoMoverme = true;
    }

    //Aproxima mi posicion para que sea un numero exacto

    public void Reposiciona()
    {
        if(transform.parent != null)
        {
            
            transform.localPosition = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), Mathf.Round(transform.localPosition.z));
        }
        else
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        }
        
    }
}
