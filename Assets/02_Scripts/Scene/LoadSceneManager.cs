using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image logoImage;
    [SerializeField] Image progressBar;
    
    public float speed = 1.0f;
    public float maxAlpha = 1.0f;
    public float minAlpha = 0.0f;

    private void Start()
    {        
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LOADING_PT_3");
    }

    IEnumerator LoadScene()
    {        
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        

        while (!op.isDone)
        {
            yield return null;
            StartCoroutine(BlinkLogo());
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    
                    yield return new WaitForSeconds(10.0f);
                    op.allowSceneActivation = true;
                    StopCoroutine(BlinkLogo());
                    yield break;
                }
            }
        }
    }

    IEnumerator BlinkLogo()
    {        
        while (true)
        {
            yield return null;

            float alpha = Mathf.PingPong(Time.time * speed, maxAlpha - minAlpha) + minAlpha;            

            Color color = logoImage.color;           
            color.a = alpha;
            logoImage.color = color;
        }
    }
}
