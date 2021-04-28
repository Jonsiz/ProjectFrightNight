using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveAndLoadManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fadePanel;
    private Image fadeImage;
    private bool loading;
    private float fadeSpeed = 0.01f;
    private Color alpha;
    [SerializeField]
    private int waitTime = 4;
    void Awake()
    {
        fadeImage = fadePanel.GetComponent<Image>();
        alpha = fadeImage.color;
    }

    void FixedUpdate()
    {
        
        if (loading)
        {
            if (alpha.a < 1)
            {
                alpha.a += fadeSpeed;
            }
            else if (alpha.a >= 1)
            {
                alpha.a = 1;
                //StartCoroutine("LoadingTime");
                PixelCrushers.SaveSystem.LoadFromSlot(1);
            }
        }
        else
        {
            if (alpha.a > 0)
            {
                alpha.a -= fadeSpeed;
            }
            else if (alpha.a <= 0)
            {
                alpha.a = 0;
            }
        }
        fadeImage.color = alpha;
    }

    void SavingdaGame()
    {
        PixelCrushers.SaveSystem.SaveToSlot(1);
    }

    public void LoadingdaGame()
    {
        loading = true;
    }

    public void ImmidiateLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //IEnumerator LoadingTime()
    //{
    //    yield return new WaitForSeconds(waitTime);

    //    loading = false;
    //}
}
