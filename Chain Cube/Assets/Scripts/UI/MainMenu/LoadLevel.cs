using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    [SerializeField] private int _levelToLoad;
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadGame);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(_levelToLoad);
    }
}
