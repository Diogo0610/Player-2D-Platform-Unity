using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2DPlatform : MonoBehaviour
{
    [SerializeField] float speed, jumpForce;
    private Rigidbody2D rb;
    SpriteRenderer sr;
    public Text lifeText;
    public int life;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        Life();
    }
    void Movement() 
    {
        if (Input.GetKey(KeyCode.D))                                           // Movimentação e troca de lado
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            sr.flipX = true;
        }                                                                      //
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) // Ativando a animação de corrida
        {                                                       //
            anim.SetBool("Running", true);                      //
        }                                                       //
        else                                                    //
        {                                                       //
            anim.SetBool("Running", false);                     //
        }                                                       //

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f) // pulo
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }                                                                     //
    }
    void Life()
    {
        lifeText.text = "X " + life;

        if (life == 0)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Acid"))
        {
            
            life--;
        }
        if (collision.gameObject.CompareTag("Laser"))
        {
            Debug.Log("Auch!");
            life--;
        }
    }
}
