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
        if (DebugVelocity != null)
        {
            Debug.DrawLine(_rigidbody2D.position, _rigidbody2D.position + (_rigidbody2D.velocity),DebugVelocity.Value);
        }
        if (DebugUp != null)
        {
            Debug.DrawLine(_rigidbody2D.position, _rigidbody2D.position + ((Vector2)_rigidbody2D.transform.up),DebugUp.Value);    
        }
    }

    private Color? DebugVelocity = Color.blue;
    private Color? DebugUp;
    private Color? DebugThrustDir;
    
    private void FixedUpdate()
    {
        var rotationRad = (_rigidbody2D.rotation ) * (Mathf.PI/180f);
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
        var thrustDirection = new Vector2(xFactor,yFactor);
        var thrustInput = Input.GetAxis("Vertical");
        Thrust = thrustDirection * (thrustInput * (2f*_rigidbody2D.mass));

        if (DebugThrustDir != null)
        {
            Debug.DrawLine(_rigidbody2D.position, _rigidbody2D.position + (thrustInput*thrustDirection),DebugThrustDir.Value);    
        }

        //invert horizontal axis because it feels more natural
        var rotationSpeed = -Input.GetAxis("Horizontal") * (_rigidbody2D.mass/20f);

        _rigidbody2D.AddTorque(rotationSpeed, ForceMode2D.Force);
        _rigidbody2D.AddForce(Thrust );
    }
}
