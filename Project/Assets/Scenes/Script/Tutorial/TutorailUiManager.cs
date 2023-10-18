using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorailUiManager : MonoBehaviour
{
    public static TutorailUiManager instance
    {
        get
        {
            if (tutorailUiManagerSingleTon == null)
            {
                tutorailUiManagerSingleTon = FindObjectOfType<TutorailUiManager>();
            }
            return tutorailUiManagerSingleTon;
        }
    }
    private static TutorailUiManager tutorailUiManagerSingleTon;


    public TextMeshProUGUI areaResult;

    public GameObject inamges;
    public GameObject result;
    public GameObject pasueButton;
    public GameObject settingUi;

    public Slider cameraDistance;
    public Slider sens;
    public GameObject err;
    private bool errActive;
    public bool IsClear
    {
        get; set;
    }
    public bool IsPause
    {
        get;set;
    }
    public void SetAreaResult(float result)
    {
        areaResult.text = $"Result : {result}%";
    }

    private void Awake()
    {
        cameraDistance.value = OnGameData.instance.CameraDistance;
        sens.value = OnGameData.instance.Sensitivity;

    }
    private void Update()
    {
        if( IsClear )
        {
            inamges.SetActive(false);
            result.SetActive(true);
            OnGameData.instance.IsTutorialClear = true;
            OnGameData.instance.Save();
        }

        if (errActive)
        {
            if(Input.GetMouseButtonDown(0)) 
            {
                err.SetActive(false);
            }
        }

    }

    public void MainLoadScene()
    {
        if(OnGameData.instance.IsTutorialClear)
            SceneManager.LoadScene(OnGameData.instance.MainSceneName);
        else
        {
            err.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("chapter 0-0 tutorial");
    }

    public void Pasue()
    {
        if (!IsClear)
        {
            pasueButton.SetActive(false);
            Time.timeScale = 0f;
            settingUi.SetActive(true);
            IsPause=true;
            GameManager.instance.playerManager.GetPlayerShootPainter().IsPause = IsPause;
        }
    }

    public void PasueExist()
    {
        Time.timeScale = 1f;
        pasueButton.SetActive(true);
        settingUi.SetActive(false);

        IsPause=false;
        GameManager.instance.playerManager.GetPlayerShootPainter().IsPause = IsPause;
    }

    public void CamerDistanceController()
    {
        OnGameData.instance.CameraDistance = cameraDistance.value;
        GameManager.instance.cameraaMove.DistanceFromPlayer = cameraDistance.value; 
    }

    public void SenstiveController()
    {
        OnGameData.instance.Sensitivity = sens.value;
        GameManager.instance.XRoationSpeed = sens.value;
    }
}
