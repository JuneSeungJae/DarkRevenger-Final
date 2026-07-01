using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyConfigUI : MonoBehaviour
{
    public Text[] keyLabels; // 키 라벨을 표시할 UI Text 배열
    private int waitingForKeyIndex = -1; // 대기 중인 키 인덱스
    

    void Start()
    {
        UpdateKeyLabels();
        
    }

    void Update()
    {
        if (waitingForKeyIndex >= 0)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    KeyManager.SetKey(waitingForKeyIndex, keyCode);
                    waitingForKeyIndex = -1;
                    UpdateKeyLabels();
                    break;
                }
            }
        }
    }

    // 키 설정 버튼을 클릭할 때 호출되는 메서드
    public void OnKeyButtonClicked(int index)
    {
        waitingForKeyIndex = index;
        keyLabels[index].text = "Press any key...";
    }

    // 키 라벨을 업데이트하는 메서드
    void UpdateKeyLabels()
    {
        for (int i = 0; i < keyLabels.Length; i++)
        {
            keyLabels[i].text = KeyManager.GetKey(i).ToString();
        }
    }
}
