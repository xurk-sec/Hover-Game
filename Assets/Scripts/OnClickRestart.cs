using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickRestart : MonoBehaviour
{
    public void OnClick(){
        this.gameObject.SetActive(false);   //设置按钮不可见
        Time.timeScale = 0; // 取消暂停
        SceneManager.LoadScene(0);  //重置场景
        // GameObject.Find("start").SendMessage("OnClick");
    }
}
