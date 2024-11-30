using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger
{
    static SceneChanger instance;

    public static SceneChanger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SceneChanger();                
            }
            return instance;
        }
    }

    public void ChangeScene(SceneType sceneType)
    {
        LoadSceneManager.LoadScene(sceneType);
    }
}
