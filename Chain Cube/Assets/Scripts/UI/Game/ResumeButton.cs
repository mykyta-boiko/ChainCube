using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnButtonClickHandler);
    }

    private void OnButtonClickHandler()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
