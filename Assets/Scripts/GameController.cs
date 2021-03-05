//score nao pode ficar no personagem para que não perca a pontuação
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//biblioteca para teletransporte de cena

public class GameController : MonoBehaviour
{
    public int totalScore;
    public Text scoreText;//texto só funciona com a UI

    public GameObject gameOver;//referencia da imagem game over no UI

    public static GameController instance;
    
    void Start()
    {
        instance = this; //atribuição da variavel ao proprio script totalScore
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void RestartGame(string levelName)//string levelName é o campo invisivel do botao
    {
        SceneManager.LoadScene(levelName);
    }
}