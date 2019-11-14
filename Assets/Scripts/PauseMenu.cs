using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject optionsScreen;
    public GameObject pauseScreen;
    public string mainMenuScene;
    public bool isPause;
    public bool canUseEsc;

    public GameObject loadingScreen, loadingIcon;
    public Text loadingText;
    public AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        canUseEsc = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canUseEsc && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (isPause == false)
        {
            pauseScreen.SetActive(true);
            isPause = true;
            Time.timeScale = 0.0F;
        }
        else
        {
            pauseScreen.SetActive(false);
            isPause = false;
            Time.timeScale = 1.0F;
        }
    }
    
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        canUseEsc = false;
        StartCoroutine(LoadMain());
    }

    public IEnumerator LoadMain()
    {
        loadingScreen.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuScene);
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
