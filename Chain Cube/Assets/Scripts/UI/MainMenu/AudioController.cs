using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _effectsButton;
    [SerializeField] private AudioMixerGroup _audioMixer;

    private bool _musicIsOn = true;
    private bool _effectsIsOn = true;
    private void Awake()
    {
        _musicButton.onClick.AddListener(MusicChanger);
        _effectsButton.onClick.AddListener(EffectsChanger);
    }

    private void MusicChanger()
    {
        if(_musicIsOn)
            _audioMixer.audioMixer.SetFloat("Music", -80);
        else
            _audioMixer.audioMixer.SetFloat("Music", 0);
        _musicIsOn = !_musicIsOn;
    }
    private void EffectsChanger()
    {
        if (_effectsIsOn)
            _audioMixer.audioMixer.SetFloat("Effects", -80);
        else
            _audioMixer.audioMixer.SetFloat("Effects", 0);
        _effectsIsOn = !_effectsIsOn;
    }
}
