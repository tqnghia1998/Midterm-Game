using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public GameObject optionsScreen;
    public GameObject loadingScreen, loadingIcon;
    public Text loadingText;
    public AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(LoadGame());
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadGame()
    {
        loadingScreen.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(firstLevel);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.isDone == false)
        {
            if (asyncLoad.progress >= 0.9F)
            {
                loadingText.text = "Press any key to continue";
                loadingIcon.SetActive(false);

                if (Input.anyKeyDown == true)
                {
                    asyncLoad.allowSceneActivation = true;
                    Time.timeScale = 1.0F;
                }
            }

            yield return null;
        }
    }

    public void playSfx()
    {
        sfx.Play();
    }
}
