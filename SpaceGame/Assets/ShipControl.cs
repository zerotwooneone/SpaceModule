using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public Vector2 Thrust;
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var rotationRad = (_rigidbody2D.rotation + (Mathf.PI / 2)) * (Mathf.PI/180);
        //sin and cos are flipped because our origin is verticle
        var xFactor = -Mathf.Sin(rotationRad);
        var yFactor = Mathf.Cos(rotationRad);
        Thrust = new Vector2(xFactor,yFactor) * Input.GetAxis("Vertical");
                
        //invert horizontal axis because it feels more natural
        var rotationSpeed = -Input.GetAxis("Horizontal");

        //_rigidbody2D.rotation += rotationSpeed* 100* Time.deltaTime;
        _rigidbody2D.AddTorque(_rigidbody2D.mass * rotationSpeed* Time.deltaTime, ForceMode2D.Force);
        //_rigidbody2D.position += thrust * (20f * Time.deltaTime);
        _rigidbody2D.AddForce(Thrust * (500f * _rigidbody2D.mass * Time.deltaTime));
    }
}
