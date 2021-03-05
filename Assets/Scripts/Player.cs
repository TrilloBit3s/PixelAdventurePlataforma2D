using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;//velocidade
    public float JumpForce;//força de pulo
    
    public bool isJumping;//esta pulando
    public bool doubleJump;//pulo duplo

    private Rigidbody2D rig;
    private Animator anim;
    
    bool isBlowing;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        //Vector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); //eixos 0y e 0Z movimentação lateral
        
        //move o personagem em uma posição
        //transform.position += moviment * Time.deltaTime * Speed; //adiciona velocidade
        
        float movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement *  Speed, rig.velocity.y);//recebe o Input.GetAxis e bloqueia o Y

        if(movement > 0f)
        {
            anim.SetBool("walk", true);//animação de andar para direita
            transform.eulerAngles = new Vector3(0f,0f,0f);//direita
        }

        if(movement < 0f)
        {
            anim.SetBool("walk", true);//animação de andar para
            transform.eulerAngles = new Vector3(0f,180f,0f);//esquerda
        }

        if(movement == 0f)
        {
            anim.SetBool("walk", false);//animação parado
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isBlowing)
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }      
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Saw")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            isBlowing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            isBlowing = false;
        }
    }
}