using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 人物射出的子弹

public class shoot : MonoBehaviour
{
    public Rigidbody Bullet;

    public Rigidbody FPonit;
    public float bullet_speed;
    public float fire_rate;
    private float count_time;
    // weapon player
    public AudioClip AC;
    
    

    // Start is called before the first frame update
    void Start()
    {
        count_time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        count_time += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && count_time >= fire_rate){
            
            FPonit.position = FPonit.transform.parent.gameObject.transform.position + Vector3.up * 1.5f;

            Rigidbody clone;

            clone = (Rigidbody)Instantiate(Bullet,FPonit.position,FPonit.rotation);
            Debug.Log("clone"+clone);
            clone.velocity = transform.TransformDirection(Vector3.forward * bullet_speed); 
            // weapon player audio
            AudioSource.PlayClipAtPoint(AC, transform.localPosition); 

            // reset
            count_time = 0.0f;
        }
    }

    
}
