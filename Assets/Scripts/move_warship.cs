using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class move_warship : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public Rigidbody rd;
    public float distance;
    public float height;
    public float rotationDamping;

    private Transform camera;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        // 注意这里把transform也写进去了
        camera = Camera.main.transform;

    }

    void Update()
    {
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

    
   
}
