using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private TMP_Text _loseText;
    [SerializeField] private CubeCreating _cubeCreating;

    private const string ADDITIONAL_TEXT = " points";
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Cube" && !other.gameObject.GetComponent<MovingCube>().IsUsed)
        {
            _loseMenu.SetActive(true);
            _loseText.text += "\n" + _cubeCreating.ScoreCount + ADDITIONAL_TEXT;
            Time.timeScale = 0;
        }
    }
}
