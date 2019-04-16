using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
    public class PlayeController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    private PlayerMotor motor;
    
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
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
    }
}
