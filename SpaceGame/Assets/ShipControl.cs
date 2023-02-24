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
        var rotationRad = (_rigidbody2D.rotation + (Mathf.PI / 2f)) * (Mathf.PI/180f);
        if (Mathf.Approximately(rotationRad, 0))
        {
            rotationRad = 0;
        }
        //sin and cos are flipped because our origin is verticle
        var xFactor = -Mathf.Sin(rotationRad);
        if (Mathf.Approximately(xFactor, 0))
        {
            xFactor = 0;
        }
        var yFactor = Mathf.Cos(rotationRad);
        Thrust = new Vector2(xFactor,yFactor) * Input.GetAxis("Vertical");
                
        //invert horizontal axis because it feels more natural
        var rotationSpeed = -Input.GetAxis("Horizontal");

        _rigidbody2D.AddTorque(rotationSpeed, ForceMode2D.Force);
        _rigidbody2D.AddForce(Thrust * (2f*_rigidbody2D.mass));
    }
}
