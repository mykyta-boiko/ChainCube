using UnityEngine;

public class ButtonLanguage : MonoBehaviour
{
    [SerializeField]
    private LocalizationManager _localizationManager;

    public void OnButtonClick()
    {
        _localizationManager.CurrentLanguage = name;
        _localizationManager.LoadLocalizedText(name);
    }
}
