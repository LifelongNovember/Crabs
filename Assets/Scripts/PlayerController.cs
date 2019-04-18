using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private float speed = 3f;
    private PlayerMotor motor;
    public int playerId;
    public Transform spawnPosition;


    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        playerId = ((int) netId.Value) % 2;
        Transform mesh = transform.Find("Mesh");
        mesh.GetChild(playerId).gameObject.SetActive(true);
        playerId++;
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
