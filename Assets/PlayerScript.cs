using Unity.Android.Gradle.Manifest;
using UnityEngine;




public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float gravityStrength = 1;
    [SerializeField] private float terminalVelocity = 16;
    [SerializeField] private float gravityChangeCD = 2;

    private Rigidbody2D rb;
    private Vector2 gravityVector = new Vector2(0, -9.8f);
    private float gravityChangeTimer = 0;
    private bool movementStatus = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GravityController();
    }
    private void FixedUpdate()
    {
        // function is called in fixed update so continuously applies force 
        CustomGravity();
    }

    private void DashMechanic()
    {

    }
    private void GravityController()
    {
        if (movementStatus)
        {
            if (Input.GetKey(KeyCode.W))
            {
                UpdateGravity(new Vector2(0, gravityStrength * 9.8f));
            }
            if (Input.GetKey(KeyCode.S))
            {
                UpdateGravity(new Vector2(0, gravityStrength * -9.8f)); //when you press a key it uses update gravity to change gravity 
            }
            if (Input.GetKey(KeyCode.A))
            {
                UpdateGravity(new Vector2(gravityStrength * -9.8f, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                UpdateGravity(new Vector2(gravityStrength * 9.8f, 0));
            }
        }
    }
    private void CustomGravity()
    {
        // here it applies force 
        rb.AddForce(gravityVector); // we can change this gravity vector by using UpdateGravity Function
        
        // Terminal Velocity
        rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.x,-terminalVelocity,terminalVelocity),
                                        Mathf.Clamp(rb.linearVelocity.y,-terminalVelocity,terminalVelocity));   
    }
    public void UpdateGravity(Vector2 newGravity,bool overrideCD = false)
    {
        if (overrideCD || Time.time - gravityChangeTimer > gravityChangeCD) // this is cooldown so players dont spam change gravity
        {
            gravityVector = newGravity;
            if (!overrideCD)
            {
                gravityChangeTimer = Time.time;
            }
        }
    }

}
