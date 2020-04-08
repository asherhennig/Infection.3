using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text bubblegumText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void SetMoneyText(int bubblegum)
    {
        bubblegumText.text = "Bubblegum: " + bubblegum;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
