using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravityFall : MonoBehaviour
{
    [SerializeField] float brakeForce = 100f;

    public float walkDeacceleration = 5f;
    public float walkDeaccelerationOnX; 
    public float walkDeaccelerationOny; 
    Rigidbody rigidbody;

    void Start(){
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)){
                Debug.Log("Left Shift");
                rigidbody.velocity = new Vector3(Mathf.SmoothDamp(rigidbody.velocity.x, 0, 
                                                 ref walkDeaccelerationOnX,
                                                 walkDeacceleration),
                                                 Mathf.SmoothDamp(rigidbody.velocity.y, 0,
                                                 ref walkDeaccelerationOny,
                                                 walkDeacceleration),
                                                0);
            }
    }

}
