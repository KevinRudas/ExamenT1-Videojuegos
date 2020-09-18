using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunayController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(8,rb.velocity.y);
    }
}
