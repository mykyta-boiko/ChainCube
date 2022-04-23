using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnButtonClickHandler);
    }

    private void OnButtonClickHandler()
    {
        if (_pauseMenu.active)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
        _pauseMenu.SetActive(!_pauseMenu.active);
    }
}
