using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoDude : MonoBehaviour
{
    public float speed;//velocidade do inimigo se movimenta

    private Rigidbody2D rig;
    private Animator anim;//manipular animação de morte do inimigo

    public Transform rightCol;//objetos para detectar colisão 
    public Transform leftCol;//objetos para detectar colisão 
    public Transform headPoint;//colisor na cabeça para saber quando o player pisou em cima

    private bool colliding;//detectar quando o inimigo colide na parede ou não

    public LayerMask layer;

    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    void Start()
    {
        rig= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);//nao altera o eixo Y
        
        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);//detecta se bateu em algo

        if(colliding)//toda vez que o inimigo colide ele vira
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);//rotacionando o inimigo
            speed *= -1;
        }
    }

    bool playerDestroyed = false;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            float height = col.contacts[0].point.y - headPoint.position.y;
           //Debug.Log(height);//se for Negativo Destroi o Player, se for positivo Destroi o Inimigo
            if (height > 0 && !playerDestroyed)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4, ForceMode2D.Impulse);
                speed = 0;
                anim.SetTrigger("TriggerDie");
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                rig.bodyType = RigidbodyType2D.Kinematic;
                //Debug.Log("matou o Inimigo");

                Destroy(gameObject, 0.25f);
            }else
            {
                playerDestroyed = true;
                GameController.instance.ShowGameOver();
                Destroy(col.gameObject);
                //Debug.Log("matou o jogador");
            }
        }
    }
}