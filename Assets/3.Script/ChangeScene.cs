using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (ScoreValue.Score >= 10) //3점 이상이면
        {
            SceneManager.LoadScene("NextStage");
        }
    }
}
