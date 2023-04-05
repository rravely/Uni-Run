using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    //Gameover 여부 판단
    //Text 활성화 부분 만들어야 함
    //스코어 계산 

    public bool isGameover = false;
    public Text ScoreText;
    public GameObject gameover_UI;
    //public int Score = 0;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Debug.Log("이미 게임 매니저는 존재합니다.");
            Destroy(gameObject); //오브젝트, 컴포넌트 삭제 가능
        }
    }

    private void Start()
    {
        ScoreText.text = "SCORE: " + ScoreValue.Score;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            ScoreValue.Score = 0;
            SceneManager.LoadScene("SampleScene");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int score) 
    {
        if (!isGameover) 
        {
            ScoreValue.Score += score;
            ScoreText.text = "SCORE: " + ScoreValue.Score;
        }
    }

    public void PlayerDead()
    {
        isGameover = true;
        gameover_UI.SetActive(true);
    }
}
