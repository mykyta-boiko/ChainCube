using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private string _currentLanguage;
    private Dictionary<string, string> _localizedText;
    private static bool _isReady = false;

    public delegate void ChangeLangText();
    public event ChangeLangText OnLanguageChanged;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
            }
            else
            {
                PlayerPrefs.SetString("Language", "en_US");
            }
        }
        _currentLanguage = PlayerPrefs.GetString("Language");
        LoadLocalizedText(_currentLanguage);
    }

    public string CurrentLanguage
    {
        get
        {
            return _currentLanguage;
        }
        set
        {
            PlayerPrefs.SetString("Language", value);
            _currentLanguage = PlayerPrefs.GetString("Language");
        }
    }
    public bool IsReady
    {
        get
        {
            return _isReady;
        }
    }
    public string GetLocalizedValue(string key)
    {
        if (_localizedText.ContainsKey(key))
        {
            return _localizedText[key];
        }
        else
        {
            throw new Exception("Localized text with key \"" + key + "\" not found");
        }
    }

    public void LoadLocalizedText(string langName)
    {
        string path = Application.streamingAssetsPath + "/Languages/" + langName + ".json";
        string dataAsJson;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            WWW reader = new WWW(path);
            while (!reader.isDone) { }

            dataAsJson = reader.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(path);
        }

        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        _localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.Items.Length; i++)
        {
            _localizedText.Add(loadedData.Items[i].Key, loadedData.Items[i].Value);
        }

        PlayerPrefs.SetString("Language", langName);
        _currentLanguage = PlayerPrefs.GetString("Language");
        _isReady = true;
        OnLanguageChanged?.Invoke();
    }
}