
using UnityEngine;

public class Player : MonoBehaviour
{   
    public Bullet bulletPrefab;
    public float _thrustSpeed= 1.0f;
    public float _turnSpeed= 1.0f;

    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    { // For checking Input
      // Update gets called every single frame the game is running 
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if ( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
        {
            _turnDirection=1.0f;  
        }
        else if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
        {
            _turnDirection= -1.0f;
        }
        else
        {
            _turnDirection= 0.0f;
        }

        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    { // FixedUpdate gets called at fixed intervals
      // Method applies physics to the object
       if(_thrusting)
       {
        _rigidbody.AddForce(this.transform.up * this._thrustSpeed);
       }
      
       if(_turnDirection != 0.0f)
       {
        _rigidbody.AddTorque(_turnDirection * this._turnSpeed);
       }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();  // Bad way, degrades performance of the game
            
        }

    }
}
