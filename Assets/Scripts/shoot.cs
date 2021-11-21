using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public Rigidbody Bullet;

    public Rigidbody FPonit;
    public float bullet_speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            
            FPonit.position = FPonit.transform.parent.gameObject.transform.position + Vector3.up * 1.5f;

            Rigidbody clone;

            clone = (Rigidbody)Instantiate(Bullet,FPonit.position,FPonit.rotation);

            clone.velocity = transform.TransformDirection(Vector3.forward * bullet_speed); 

        }
    }
}
