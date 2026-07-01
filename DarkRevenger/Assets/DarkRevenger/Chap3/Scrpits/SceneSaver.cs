using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class SceneSaver : MonoBehaviour
{
    private string filePath;

    public string mainMenuSceneName = "Main";  // 메인화면 씬의 이름

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "savedSceneName.json");
        
        if (SceneManager.GetActiveScene().name != mainMenuSceneName)
        {
            SaveSceneName(filePath);  // 메인화면이 아닌 경우에만 씬 이름 자동 저장
        }
    }

    public void SaveSceneName(string filePath)
    {
        // 현재 씬 이름 가져오기
        string sceneName = SceneManager.GetActiveScene().name;

        // 씬 이름을 저장할 데이터 객체 생성
        SceneNameData sceneNameData = new SceneNameData { sceneName = sceneName };

        // 씬 이름 데이터를 JSON 형식으로 변환
        string json = JsonUtility.ToJson(sceneNameData, true);

        // JSON 데이터를 파일에 저장
        File.WriteAllText(filePath, json);

        Debug.Log("Scene name saved to " + filePath);
    }

    public void LoadSavedScene()
    {
        // 파일이 존재하는지 확인
        if (File.Exists(filePath))
        {
            // JSON 파일에서 씬 이름 읽기
            string json = File.ReadAllText(filePath);
            SceneNameData sceneNameData = JsonUtility.FromJson<SceneNameData>(json);

            // 저장된 씬 이름으로 씬 로드
            SceneManager.LoadScene(sceneNameData.sceneName);
        }
        else
        {
            Debug.LogError("Save file not found at " + filePath);
        }
    }
}

[System.Serializable]
public class SceneNameData
{
    public string sceneName;
}