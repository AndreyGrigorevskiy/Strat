using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [Header("Загружамеая сцена")]
    public int sceneID;

    [Header("Объекты UI")]
    public Image loadingImage;
    public Text loadingText;

    void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    private IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while(!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingImage.fillAmount = progress;
            loadingText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }
        
    }

}
