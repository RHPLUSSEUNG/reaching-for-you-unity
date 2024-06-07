using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    TITLE,
    AM,
    PM,
    BATTLE,
}

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
        switch (sceneType)
        {
            case SceneType.TITLE:
                {                    
                    SceneManager.LoadScene("TITLE_PT_3");
                    SoundManager.Instance.PlayMusic("Main_Theme");
                    break;
                }
            case SceneType.AM:
                {                    
                    SceneManager.LoadScene("AM_PT_3");
                    SoundManager.Instance.PlayMusic("AM_School");
                    break;
                }
            case SceneType.PM:
                {
                    break;
                }
            case SceneType.BATTLE:
                {                    
                    SceneManager.LoadScene("Battle_PT_3");
                    SoundManager.Instance.PlayMusic("Stage1_Battle");
                    break;
                }
        }        
    }
}
