using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pause_menu;
    public static bool game_is_paused;
    // Start is called before the first frame update
    void Start()
    {
        game_is_paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
    }

    public void Pause() {
        if (!game_is_paused) {
            pause_menu.SetActive(true);
            Time.timeScale = 0f;
            game_is_paused = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            pause_menu.SetActive(false);
            Time.timeScale = 1f;
            game_is_paused=false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    public void LoadScene(string sceneName) {
        game_is_paused = false;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
