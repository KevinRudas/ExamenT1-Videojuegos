using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    private const int ANIMATION_QUIETO = 0;
    private const int ANIMATION_CAMINAR = 1;
    private const int ANIMATION_SALTAR = 2;
    private const int ANIMATION_LANZAR = 3;
    private const int ANIMATION_MORIR = 4;
    
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    public GameObject kunaiDerecho;
    [CanBeNull] public Transform kunaiDer;
    public GameObject kunaiIzquierdo;
    [CanBeNull] public Transform kunaiIzq;
    private int direccion_kunai = 0;

    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetInteger("Estado", ANIMATION_QUIETO);
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(10, rb.velocity.y);
            animator.SetInteger("Estado", ANIMATION_CAMINAR);
            sr.flipX = false;
            direccion_kunai = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-10, rb.velocity.y);
            animator.SetInteger("Estado", ANIMATION_CAMINAR);
            sr.flipX = true;
            direccion_kunai = 1;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetInteger("Estado", ANIMATION_SALTAR);
            rb.AddForce(new Vector2(0, 30), ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetInteger("Estado", ANIMATION_LANZAR);
            if (direccion_kunai == 0)
            {
                Instantiate(kunaiDerecho, kunaiDer.transform.position, quaternion.Euler(0, 0, 0));
            }
            if (direccion_kunai == 1)
            {
                Instantiate(kunaiIzquierdo, kunaiIzq.transform.position, quaternion.Euler(0, 0, -135));
            }
        }
    }

    void Morir()
    {
        animator.SetInteger("Estado", ANIMATION_MORIR);
        Invoke("DestruirPersonaje", 1f);
    }

    void DestruirPersonaje()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "zombie")
        {
            Invoke("Morir", 0.05f);
        }
    }
}
