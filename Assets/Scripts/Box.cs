using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float jumpForce;
    public bool isUp;

    public int health = 3;

    public Animator anim;
    
    void Update()
    {
       if(health <= 0)
       {
           Destroy(gameObject);  
       }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(isUp)
            {
                anim.SetTrigger("TriggerHit");
                health--;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);        
            }else
            {
                anim.SetTrigger("TriggerHit");
                health--;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Impulse);
            }  
        }
    }
}