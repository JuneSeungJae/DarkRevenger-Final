using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject pausePanel; //게임정지
    public GameObject Pause; // 옵션창으로 가는 중간단계
    public GameObject optionPanel; // 옵션창
    public GameObject keyOption; // 키 설정창
    [SerializeField] public string SceneName; // 전환할 씬의 이름

    // 아래는 옵션버튼을 눌렀을 때나 X버튼을 눌러 창을 끌 때 쓰는 코드
    public void OffPausePanel() {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnPause() {
        Pause.SetActive(true);
    }

    public void OffPause() {
        Pause.SetActive(false);
    }

    public void OnOption(){
        optionPanel.SetActive(true);
    }

    public void OffOption(){
        optionPanel.SetActive(false);
    }

    public void OnKeyOption(){
        keyOption.SetActive(true);
    }

    public void OffKeyOption(){
        keyOption.SetActive(false);
    }
    public void OnMain(){
        SceneManager.LoadScene(SceneName);
    }
    public void OnCanvas(){
        canvas.SetActive(true);
    }

    void Start(){
        Time.timeScale = 1;
    }
    void Update() { //esc를 눌렀을 때 옵션창이 순서대로 꺼진다.
        if (Input.GetButtonDown("Cancel")) {
            if (pausePanel.activeSelf==true && optionPanel.activeSelf==false && keyOption.activeSelf==false){
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                canvas.SetActive(true);
            }
            else if(keyOption.activeSelf==true){
                keyOption.SetActive(false);
                optionPanel.SetActive(true);
            }
            else if(optionPanel.activeSelf==true){
                optionPanel.SetActive(false);
                Pause.SetActive(true);
            }
            else if(pausePanel.activeSelf==false)
            {
                Time.timeScale = 0;
                canvas.SetActive(false);
                pausePanel.SetActive(true);
                Pause.SetActive(true);
            }
        }
    }
}
