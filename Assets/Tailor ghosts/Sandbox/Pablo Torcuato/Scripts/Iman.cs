﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman : MonoBehaviour
{
    RaycastHit2D hitRight;
    //Con esto hacemos que una variable privada se vea en el editor [SerializeField]
    public LayerMask capas;
    public float distancia = 1f;
    public float velocidad = 5f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.right * distancia, Color.red);
        hitRight = Physics2D.Raycast(transform.position, transform.right, distancia, capas);
        if (hitRight)
        {
            
            if (hitRight.transform.CompareTag("Normal"))
            {
                hitRight.rigidbody.velocity = Vector2.zero;
                Debug.DrawRay(transform.position, transform.right * distancia, Color.green);
                Vector2 miPosicion = new Vector2(transform.position.x +1f, transform.position.y);
                Vector2 posicionDelNormal = new Vector2(hitRight.transform.position.x, hitRight.transform.position.y);
                hitRight.transform.position = Vector2.MoveTowards(posicionDelNormal, miPosicion, velocidad * Time.deltaTime);
            }
        }
    }
}