using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turrets : MonoBehaviour
{
    public GameObject turret;
    public Rigidbody Bullet;
    public float bullet_speed;
    // 射击间隔
    public float interval_time;
    // 间隔计时
    private float invoke_time;
    private GameObject[] arr = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
      // 动态创建turret
      GameObject clone;
      for(int i = 0; i < 5; i++){

        int ax = Random.Range(-83, 83);
        int az = Random.Range(-110, 110);

        clone = Instantiate(turret); 
        clone.transform.position = new Vector3(ax, 0.5f, az);
        arr[i] = clone;
      }
    }

    // Update is called once per frame
    void Update()
    {
      invoke_time += Time.deltaTime;
      if(invoke_time >= interval_time){
        
        for(int j = 0; j < 5; j++){
          Rigidbody bull;
          bull = (Rigidbody)Instantiate(Bullet, arr[j].transform.position + Vector3.up * 3, arr[j].transform.rotation);

          bull.velocity = arr[j].transform.TransformDirection(Vector3.forward * bullet_speed);   
        } 
        invoke_time = 0.0f;
      }
    }
}
