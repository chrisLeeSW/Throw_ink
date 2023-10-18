using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerUiManager : MonoBehaviour
{
   private enum ColorRGB
    {
        R, G, B
    }

   public List<Image> mainImages = new List<Image>();
   public List<Slider> sliders = new List<Slider>();
   public List<Image> slidersInFillImage = new List<Image>();
    public Image resultImage;

    private Color red=Color.red;
    private Color green=Color.green;
    private Color blue=Color.blue;  
    private Color resultColor = Color.white;   
    private float maxColor = 255;

    private void Awake()
    {
        OnGameData.instance.NowSceneName =SceneManager.GetActiveScene().name;
        var tempColor = OnGameData.instance.gameColor;
        red.r =tempColor.r;
        mainImages[(int)ColorRGB.R].color = red;
        slidersInFillImage[(int)ColorRGB.R].color = red;
        sliders[(int)ColorRGB.R].value = red.r * 255;

        green.g = tempColor.g;
        mainImages[(int)ColorRGB.G].color = green;
        slidersInFillImage[(int)ColorRGB.G].color = green;
        sliders[(int)ColorRGB.G].value = green.g * 255;

        blue.b = tempColor.b;
        mainImages[(int)ColorRGB.B].color = blue;
        slidersInFillImage[(int)ColorRGB.B].color = blue;
        sliders[(int)ColorRGB.B].value = blue.b * 255;
    }
    private void FixedUpdate()
    {
        float redValu = sliders[(int)ColorRGB.R].value;
        red.r = redValu / maxColor;
        mainImages[(int)ColorRGB.R].color = red;
        slidersInFillImage[(int)ColorRGB.R].color = red;

        float greenValu = sliders[(int)ColorRGB.G].value;
        green.g = greenValu / maxColor;
        mainImages[(int)ColorRGB.G].color = green;
        slidersInFillImage[(int)ColorRGB.G].color = green;

        float blueValu = sliders[(int)ColorRGB.B].value;
        blue.b = blueValu / maxColor;
        mainImages[(int)ColorRGB.B].color = blue;
        slidersInFillImage[(int)ColorRGB.B].color = blue;

        resultColor.r = red.r;
        resultColor.g = green.g;
        resultColor.b = blue.b;

        resultImage.color = resultColor;
    }


    public void ColorSave()
    {
        OnGameData.instance.gameColor = resultColor;
    }

    public void BackButton()
    {
        SceneManager.LoadScene(OnGameData.instance.PrevSceneName);
    }
    public void HomeButton()
    {
        SceneManager.LoadScene(OnGameData.instance.MainSceneName);
    }
}
