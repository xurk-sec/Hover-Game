using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class move_warship : MonoBehaviour
{
    public GameObject RestartButton;
    public GameObject ExitButton;
    public GameObject StartButton;
    public float speed;
    public float turnSpeed;
    public Rigidbody rd;
    public float distance;
    public float height;
    public float rotationDamping;
    // explosion player
    public AudioClip AC;
    // 血量展示文本
    public Text Hit_Point_Text;
    // 血量
    private static float Hit_Point;

    private Transform camera;
    private Animator animator;


    void Start()
    {
        // 暂停，等按start button后开始
        Time.timeScale = 0;
        RestartButton.SetActive(false);
        ExitButton.SetActive(true);
        StartButton.SetActive(true);

        Hit_Point = 100.0f;
        setHitPoint();

        animator = GetComponent<Animator>();
        // 注意这里把transform也写进去了
        camera = Camera.main.transform;

    }

    void Update()
    {
        // bug:
        // 1. 船与turret碰撞
        // 2. turrets被turret射出的炮弹打中也会被销毁
        // Read movement keys
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        // Turning
        if (moveHorizontal != 0) {
            this.gameObject.transform.Rotate (new Vector3 (0.0f, moveHorizontal * turnSpeed, 0.0f));
        }
        // 若撞到turret，直接游戏失败
        // Forward or backwards
        if (moveVertical != 0) {
            Vector3 fwd = transform.forward;
            GetComponent<Rigidbody> ().velocity = fwd * speed * moveVertical;
        }


        // Camera following the player
        // Calculate the wanted and current rotation angle
        float wantedRotationAngle = this.gameObject.transform.eulerAngles.y;
        float currentRotationAngle = camera.eulerAngles.y;
        // Damp the rotation around the Y-axis
        currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle,rotationDamping * Time.deltaTime);
        // Convert the angle into a quaternion
        Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
        // Set the (x,z) camera position based on where the player is
        camera.position = this.gameObject.transform.position;
        camera.position -= currentRotation * Vector3.forward * distance;
        // Set the height (y) of the camera
        Vector3 newPosition = new Vector3 (camera.transform.position.x, height, camera.transform.position.z);
        camera.position = newPosition;
        // Look at the player
        camera.LookAt (this.gameObject.transform);
    }

    void setHitPoint(){
        Hit_Point_Text.text = "Hit Point: " + Hit_Point + "/100";
    }

    void OnCollisionEnter(Collision collision){
        // 判断撞到的是子弹还是turret
        if(collision.collider.name == "sphere(Clone)"){
            Hit_Point -= 20;    // 撞到 bullet 扣 20 血
            setHitPoint();
            if(Hit_Point <= 0){
                lostGame();
            }
        } else if(collision.collider.name == "Turret(Clone)"){
            Hit_Point = 0;    // 撞到 turret 直接失败
            setHitPoint();
            if(Hit_Point <= 0){
                lostGame();
            }
        }
        
    }
    
    void lostGame(){
        Destroy(this.gameObject);
        // explosion player radio
        ParticleSystem explo = GameObject.Find("Explosion01b").GetComponent<ParticleSystem>();
        explo.transform.position = this.gameObject.transform.position;
        explo.Play();
        // weapon player audio
        AudioSource.PlayClipAtPoint(AC, transform.localPosition); 

        // show the final points
        GameObject.Find("Turret(Clone)").SendMessage("lostGameText");
        
        RestartButton.SetActive(true);
        ExitButton.SetActive(true);
        // StartButton.SetActive(true);
        // 暂停场景
        Time.timeScale = 0;
    }
   
}



// • Turrets shall shoot bullets that can damage or destroy the player. 

// • The total points shall be shown in a GUI text object.
// • When the player is destroyed a Game Over sign shall be shown.