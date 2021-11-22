using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onClickStart : MonoBehaviour
{
    public GameObject ExitButton;
    public void OnClick(){
        this.gameObject.SetActive(false);   //设置按钮不可见
        ExitButton.SetActive(false);
        Time.timeScale = 1; // 取消暂停
    }
}
