using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;
    private bool facingRight = true;
    Animator anim;
    public Text score;
    public int scoreValue;
    public bool PanMan = false;
    public int kills = 0;

    void Start()
    {
        PlayerPrefs.SetInt("ScoreValue", scoreValue);
        dest = transform.position;
        anim = GetComponent<Animator>();
        score.text = "Score: " + scoreValue.ToString();
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");

        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        if ((Vector2)transform.position == dest)
        {
            if (Input.GetKey(KeyCode.W) && valid(Vector2.up))
                dest = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.D) && valid(Vector2.right))
                dest = (Vector2)transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.S) && valid(-Vector2.up))
                dest = (Vector2)transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.A) && valid(-Vector2.right))
                dest = (Vector2)transform.position - Vector2.right;
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        if (kills >= 4)
        {
            SceneManager.LoadScene("Winner");
        }
    }

    bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerEnter2D(Collider2D co)
    {
        if (co.CompareTag("Coin"))
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            PlayerPrefs.SetInt("ScoreValue", scoreValue);
        }

        else if (co.CompareTag("Pan"))
        {
            anim.SetBool("PanMan", true);
            gameObject.transform.tag = "PanMan";
            Destroy(co.gameObject);
        }

        else if (co.CompareTag("Enemy"))
        {
            kills += 1;
        }
    }
}
