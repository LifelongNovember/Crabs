using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    public float speed = 3f;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    public GameController gc;

    void Start() 
    {
        gc = GameObject.Find("/GameManager").GetComponent<GameController>();
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Appliqué à chaque itération physique
    void FixedUpdate()
    {
        if(gc.stillPlaying) PerformMovement();
    }

    // En fonction de la velocité
    void PerformMovement()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 horizontalRotation = transform.right * xMovement;
        Vector3 verticalRotation = transform.forward * zMovement;

        // Vecteur mouvement final
        Vector3 velocity = (horizontalRotation + verticalRotation).normalized * speed;
        //motor.Move(velocity);
        if (Input.GetButton("Horizontal")||Input.GetButton("Vertical")) 
        {
            Vector3 newForward = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            if (newForward!=Vector3.zero) transform.forward = newForward;
            transform.Translate(Vector3.forward * Time.deltaTime);
        }

        if (Input.GetButton("Horizontal")||Input.GetButton("Vertical")) 
        {
            Vector3 newForward = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            if (newForward!=Vector3.zero) transform.forward = newForward;
            transform.Translate(Vector3.forward*Time.deltaTime);
        }
    }

}
