using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turrets : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject RestartButton;
    public GameObject ExitButton;
    // sum of turrets
    public static int sum_of_turrets = 5;
    // game over
    public Text gameOver;
    public GameObject turret;
    public Transform target;
    public Rigidbody Bullet;
    // weapon enemy
    public AudioClip AC;
    public float bullet_speed;
    // 射击间隔
    public float interval_time;
    // 间隔计时
    private float invoke_time;
    private GameObject[] arr = new GameObject[sum_of_turrets];
    // Start is called before the first frame update
    void Start()
    {
      // 字符串置空
      gameOver.text = "";
      // 动态创建turret
      GameObject clone;
      for(int i = 0; i < sum_of_turrets; i++){

        int ax = Random.Range(-83, 83);
        int az = Random.Range(-110, 110);

        clone = Instantiate(turret); 
        clone.transform.position = new Vector3(ax, 0.5f, az);
        arr[i] = clone;
        Debug.Log("成功生成turret");
      }
    }

    // Update is called once per frame
    void Update()
    {
      // set the interval of shoot
      invoke_time += Time.deltaTime;
      if(invoke_time >= interval_time){
        
        for(int j = 0; j < sum_of_turrets; j++){
          // check if destroyed
          if(arr[j]){
            Rigidbody bull;
            // 控制turrets朝向角色进行射击  
            arr[j].transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z)); 
            // clone bullet (不能从turrets内部射出，否则直接碰撞，销毁turrets)将turret的碰撞体变小
            // 发射位置（空物体）是turret的二级子物体
            bull = (Rigidbody)Instantiate(Bullet, arr[j].transform.GetChild(0).GetChild(0).transform.position , arr[j].transform.GetChild(0).GetChild(0).transform.rotation);
            // set the velocity of bullet
            bull.velocity = arr[j].transform.TransformDirection(Vector3.forward * bullet_speed);
            // weapon enemy
            AudioSource.PlayClipAtPoint(AC, arr[j].transform.localPosition); 
          }
        }
        // reset invoke time
        invoke_time = 0.0f;
      }
    }

    void winGame(int points){
      // check if win when per turret is destroyed.
      if(points >= sum_of_turrets){
        // win
        gameOver.text = "You win! \nYour scores are: " + points;
        RestartButton.SetActive(true);
        ExitButton.SetActive(true);
        // 暂停场景
        Time.timeScale = 0;
      }
    }
}
