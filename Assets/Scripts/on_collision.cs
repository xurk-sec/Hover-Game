using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// turret受到碰撞

public class on_collision : MonoBehaviour
{
    // game over
    public Text gameOver;
    // 得分
    public Text points_text;
    // explosion enemy
    public AudioClip AC;
    // 5个turret就有五个当前脚本，需要让脚本之间共享变量points，否则points值最高为1
    private static int points;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("0");             //
        points = 0;
        // 无法拖拽非预制体的text组件到预制体物体上，只能通过脚本获取   
        points_text = GameObject.Find("points").GetComponent<Text>();
        gameOver = GameObject.Find("gameover").GetComponent<Text>();
        Debug.Log("1");             //
        setPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision){
        Destroy(this.gameObject);
        // explosion enemy radio
        ParticleSystem explo = GameObject.Find("Explosion01b").GetComponent<ParticleSystem>();
        explo.transform.position = this.gameObject.transform.position;
        explo.Play();
        // explosion enemy audio
        AudioSource.PlayClipAtPoint(AC, this.gameObject.transform.localPosition); 
        points++;
        setPoints();
        // check if win when per turret is destoryed
        GameObject.Find("star_trek").SendMessage("winGame", points);
    }

    void setPoints(){
        points_text.text = "Points: " + points;
    }

    void lostGameText(){
        gameOver.text = "You lost! \nYour scores are: " + points;
    }
}
