using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyManager : MonoBehaviour
{
    public static KeyCode[] defaultKeys = new KeyCode[]{KeyCode.Z, KeyCode.X, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftShift};

    public static KeyCode GetKey(int index)
    {
        if (index >= 0 && index < defaultKeys.Length)
        {
            return defaultKeys[index];
        }
        else
        {
            Debug.LogError("Invalid key index!");
            return KeyCode.None;
        }
    }

    // 키 설정을 변경하고 PlayerPrefs에 저장하는 메서드
    public static void SetKey(int index, KeyCode key)
    {
        if (index >= 0 && index < defaultKeys.Length)
        {
            defaultKeys[index] = key;
            PlayerPrefs.SetInt($"Key{index}", (int)key);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("Invalid key index!");
        }
    }
}
