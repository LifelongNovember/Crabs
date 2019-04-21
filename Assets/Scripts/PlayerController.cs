﻿using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    public float speed = 3f;
    public float moveForce = 30.0f;
    public float distanceToTake;
    public float distanceToDrop;
    private PlayerMotor motor;
    public int playerId;
    public bool isX = false;
    private bool isTakingObject = false;



    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        playerId = ((int) netId.Value) % 2;
        if(playerId == 0) isX = true;
        Transform mesh = transform.Find("Mesh");
        mesh.GetChild(playerId).gameObject.SetActive(true);
        playerId++;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().drag = 4.0f;
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

        if(Input.GetMouseButtonDown(0)) {
            if(!isTakingObject) CmdRequestTakeObject(isX);
            else CmdDropObject();
        }
    }

    [Command]
    void CmdRequestTakeObject(bool _isX) {
        Transform obj;
        GameObject pool = _isX ? GameObject.Find("/X PickUps") : GameObject.Find("/O PickUps");
        for(int i = 0; i < 5; i++) {
            if(pool.transform.FindChild("" + i) != null) {
                obj = pool.transform.FindChild("" + i); 
                if(Vector3.Distance(obj.position, transform.position) < distanceToTake) {
                    RpcTakeObject(i, _isX);
                    break;
                }
            }
        }
    }

    [Command]
    void CmdDropObject() {
        bool hasDropped = false;
        GameObject container;
        for(int i = 0; i < 9; i++) {
            container = GameObject.Find("/Containers/" + i);
            if(Vector3.Distance(container.transform.position, transform.FindChild("ObjectInHand").position) < distanceToDrop && container.transform.childCount == 0) {
                RpcPlaceObjectInContainer(i, isX);
                hasDropped = true;
            }
        }
        if(!hasDropped) RpcDropObject();
    }

    [ClientRpc]
    void RpcTakeObject(int _i, bool _isX) {
        GameObject obj = _isX ? GameObject.Find("/X PickUps/" + _i) : GameObject.Find("/O PickUps/" + _i); 
        obj.transform.parent = transform.FindChild("ObjectInHand");
        obj.transform.localPosition = new Vector3(0,0,0);
        obj.transform.localRotation = new Quaternion(0,0,0,0);
        isTakingObject = true;
    }

    [ClientRpc]
    void RpcDropObject() {
        transform.FindChild("ObjectInHand").GetChild(0).parent = isX ? GameObject.Find("/X PickUps").transform : GameObject.Find("/O PickUps").transform; 
        isTakingObject = false;
    }

    [ClientRpc]
    void RpcPlaceObjectInContainer(int _i, bool _isX) {
        Transform obj = transform.FindChild("ObjectInHand").GetChild(0); 
        obj.parent = GameObject.Find("/Containers/" + _i).transform;
        obj.localPosition = new Vector3(0,0,0);
        obj.localRotation = new Quaternion(0,0,0,0);
        obj.localScale = new Vector3 (1.0f/1.5f, 1, 1);
        isTakingObject = false;        
    }
}
