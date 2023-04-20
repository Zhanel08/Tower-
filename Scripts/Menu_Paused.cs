using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Paused : MonoBehaviour
{
    [SerializeField] GameObject menu_paused;
    [SerializeField] KeyCode keyMenuPaused;

    bool _isMenuPaused = false;

    private void Start()
    {
        menu_paused.SetActive(false);
    }
    private void Update()
    {
        ActiveMenu();
    }
    public void ActiveMenu()
    {
        if(Input.GetKeyDown(keyMenuPaused))
        {
            _isMenuPaused = !_isMenuPaused;
        }
        if(_isMenuPaused)
        {
            menu_paused.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            menu_paused.SetActive(false);
            Time.timeScale = 1f;
        }

    }
    public void MenuPausedCont()
    {
        _isMenuPaused = false ;
    }
}
