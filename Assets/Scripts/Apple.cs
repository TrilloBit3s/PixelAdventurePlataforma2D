using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private SpriteRenderer sr; //sr de spriteRender
    private CircleCollider2D circle;

    public GameObject collected;
    public int Score;//pontuação do item coletavel

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();//controlam o componente
        circle = GetComponent<CircleCollider2D>();    
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")//quando encostar na maça desativa o componente do start "sr e circle"
        {
            sr.enabled = false;//desativa o componente sprite
            circle.enabled = false;//desabilita colisor
            collected.SetActive(true);//ativar a fumaça pos maça

            GameController.instance.totalScore += Score;
            GameController.instance.UpdateScoreText();
            Destroy(gameObject, 0.25f);//o g minusculo é para destruir o proprio objeto o G grande é declaração de variável
        }
    }
}