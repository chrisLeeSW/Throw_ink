using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUiManager : MonoBehaviour
{
    private enum SettingData
    {
        Sound,
        Fov,
        Sens
    }

    public List<Slider> scrollbars = new List<Slider>();
    private void Awake()
    {
        OnGameData.instance.NowSceneName = SceneManager.GetActiveScene().name;

        scrollbars[(int)SettingData.Sound].value = OnGameData.instance.SoundVolum;
    }

  
    public void LoadMainScene()
    {
        SceneManager.LoadScene(OnGameData.instance.MainSceneName);
    }
    public void BackButton()
    {
        SceneManager.LoadScene(OnGameData.instance.PrevSceneName);
    }

    public void AudioController()
    {
        OnGameData.instance.SoundVolum = scrollbars[(int)SettingData.Sound].value;
        if (scrollbars[(int)SettingData.Sound].value == -40f) OnGameData.instance.audioMixer.SetFloat("Master", -80f);
        else OnGameData.instance.audioMixer.SetFloat("Master", scrollbars[(int)SettingData.Sound].value);
    }

    public void CamerDistanceController()
    {
        OnGameData.instance.CameraDistance = scrollbars[(int)SettingData.Fov].value;
    }

    public void SenstiveController()
    {
        OnGameData.instance.Sensitivity = scrollbars[(int)SettingData.Sens].value;
    }
}
