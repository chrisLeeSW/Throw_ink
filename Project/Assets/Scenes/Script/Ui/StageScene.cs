using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageScene : MonoBehaviour
{
    public void LoadChapter1Stage1_1()
    {
        SceneManager.LoadScene("GameBuildScene"); // 바꿔야함 스테이지별로
    }
}
