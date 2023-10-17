using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    public GameObject clearGameObject;
    public GameObject failGameObject;

    public List<GameObject> satrGameObject;
    public List<GameObject> resultTextGameObject;
    public List<GameObject> rankGameObject;
    [SerializeField, Range(0, 3)]
    private int result;

    private void Awake()
    {
        // result는 GameData에서 받아오기
        switch (result)
        {
            case 0:
                failGameObject.SetActive(true);
                break;
            case 1:
                ClearTypeResult(result);
                break;
            case 2:
                ClearTypeResult(result);
                break;
            case 3:
                ClearTypeResult(result);
                break;
        }
    }
    private void ClearTypeResult(int starCount)
    {
        var type = starCount - 1;
        clearGameObject.SetActive(true);
        for (int i=0; i<=type;++i)
        {
            satrGameObject[i].SetActive(true);
        }
        resultTextGameObject[type].SetActive(true);
        rankGameObject[type].SetActive(true);
    }
    public void LoadMainLobby()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Stage1SelectScene"); // 추후에서 씬을 데이터를 받게 저장해야함
    }
}
