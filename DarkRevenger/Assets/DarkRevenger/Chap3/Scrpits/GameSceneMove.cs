using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneMove : MonoBehaviour
{
    public GameObject creditPanel; // 크래딧 팝업 창을 가리키는 GameObject
    public Button creditButton; // 크래딧 버튼
    public GameObject canvas;
    public GameObject pausePanel; //게임정지
    public GameObject optionPanel; // 옵션창
    public GameObject keyOption; // 키 설정창

    void Start()
    {
        Time.timeScale = 1;
        // 크래딧 팝업 창을 초기에 비활성화합니다.
        creditPanel.SetActive(false);
    }
    void Update()
    {
        // ESC 키를 눌렀을 때 팝업을 닫습니다.
        if (Input.GetButtonDown("Cancel")) {
            if(creditPanel.activeSelf==true && pausePanel.activeSelf==false)
            {
                creditPanel.SetActive(false);
            }
            if (pausePanel.activeSelf==true && keyOption.activeSelf==false){
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                canvas.SetActive(true);
            }
            else if(keyOption.activeSelf==true){
                keyOption.SetActive(false);
                optionPanel.SetActive(true);
            }
        }
    }

    public void OpenCreditPanel()
    {
        // 버튼을 클릭하면 크래딧 팝업 창을 활성화합니다.
        creditPanel.SetActive(true);
    }

    public void CloseCreditPanel()
    {
        // 팝업 닫기 버튼을 클릭하거나 ESC 키를 누르면 크래딧 팝업 창을 비활성화합니다.
        creditPanel.SetActive(false);
    }

    public void onClickNewGame()
    {
        SceneManager.LoadScene("CH1");
    }

    public void OnClickExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void OffPausePanel() {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
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
    public void OnMainOption(){
        pausePanel.SetActive(true);
        optionPanel.SetActive(true);
    }
}