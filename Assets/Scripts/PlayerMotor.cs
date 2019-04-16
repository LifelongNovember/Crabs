using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Appliqué à chaque itération physique
    void FixedUpdate()
    {
        PerformMovement();
    }

    // En fonction de la velocité
    void PerformMovement()
    {
        if (Input.GetButton("Horizontal")||Input.GetButton("Vertical")) 
        {
            Vector3 newForward = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            if (newForward!=Vector3.zero) transform.forward = newForward;
            transform.Translate(Vector3.forward*Time.deltaTime);
        }
    }

}
