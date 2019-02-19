﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baba : MonoBehaviour
{
    Vector2 posicionBaba;
    bool toca = false;
    Transform posicionPlayer;
    public Sprite[] spritesBabas;
    RaycastHit2D hitRight;
    RaycastHit2D hitLeft;
    RaycastHit2D hitTop;
    RaycastHit2D hitBot;
    public bool right = false;
    public bool left = false;
    public bool top = false;
    public bool bot = false;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        posicionBaba = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Comprobador();
        if (toca == true && posicionPlayer.parent == null)
        {
            posicionPlayer.position = Vector2.MoveTowards(posicionPlayer.position, posicionBaba, 0.05f);
            Invoke("Despega", 0.5f);
                
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.transform.CompareTag("Player")&&BabaController.dentro==false)
        {           
            posicionPlayer = col.gameObject.transform;
            toca = true;
            col.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            BabaController.dentro = true;

        }
       
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            {
            Invoke("Fuera", 1f);
            }
    }

    void Despega()
    {
        toca = false;
    }
    void Fuera()
    {
        BabaController.dentro = false;
    }


    public void Comprobador()
    {
        //RayCast Derecho
        Debug.DrawRay(transform.position + new Vector3(0.6f,0,0), transform.right * 0.5f, Color.red);
        hitRight = Physics2D.Raycast(transform.position + new Vector3(0.6f, 0, 0), transform.right, 0.6f);
        if (hitRight)
        {

            if (hitRight.transform.CompareTag("Baba"))
            {
                Debug.DrawRay(transform.position, transform.right * 0.6f, Color.green);
                right = true;
            }

        }
        //RayCast Izquierdo
        Debug.DrawRay(transform.position - new Vector3(0.6f, 0, 0), transform.right * -0.5f, Color.red);
        hitLeft = Physics2D.Raycast(transform.position - new Vector3(0.6f, 0, 0), transform.right * -1, 0.6f);
        if (hitLeft)
        {

            if (hitLeft.transform.CompareTag("Baba"))
            {
                Debug.DrawRay(transform.position, transform.right * -0.6f, Color.green);
                left = true;
            }

        }
        //RayCast Superior
        Debug.DrawRay(transform.position + new Vector3(0, 0.6f, 0), transform.up * 0.5f, Color.red);
        hitTop = Physics2D.Raycast(transform.position + new Vector3(0, 0.6f, 0), transform.up, 0.6f);
        if (hitTop)
        {

            if (hitTop.transform.CompareTag("Baba"))
            {
                Debug.DrawRay(transform.position, transform.up * 0.6f, Color.green);
                top = true;
            }

        }
        //RayCast Inferior
        Debug.DrawRay(transform.position - new Vector3(0, 0.6f, 0), transform.up * -0.5f, Color.red);
        hitBot = Physics2D.Raycast(transform.position - new Vector3(0, 0.6f, 0), transform.up * -1, 0.6f);
        if (hitBot)
        {

            if (hitBot.transform.CompareTag("Baba"))
            {
                Debug.DrawRay(transform.position, transform.up * -0.6f, Color.green);
                bot = true;
            }

        }


        CambiaBabas();
    }

    public void CambiaBabas()
    {
        if(left == true)
        {
            sr.sprite = spritesBabas[1];
        }
        if (right == true)
        {
            sr.sprite = spritesBabas[2];
        }
        if (left == true && right == true)
        {
            sr.sprite = spritesBabas[0];
        }
        if(top == true)
        {
            sr.sprite = spritesBabas[3];
        }
    }
}
