using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    GameObject textBalloon;
    [SerializeField]
    GameObject intro;
    [SerializeField]
    GameObject tutorial;
    [SerializeField]
    GameObject score;


    [SerializeField]
    AudioSource gameOverSound;
    [SerializeField]
    AudioSource scoreSound;
    [SerializeField]
    AudioSource reverseSound;

    [SerializeField]
    TMP_Text textScore;

    public Rigidbody2D rb;
    Vector3 startPosition;

    int highScore = 0;
    int currentScore = 0;
    int message = 1;
    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (message == 1 && highScore > 0)
        {
            message++;
        }
        if (Input.GetMouseButtonUp(0))
        {
            message++;
            if (message == 2 && highScore == 0)
            {
                message++;
            }
            if (message == 4)
            {
                message = 0;
                started = true;
            }
        }

        intro.SetActive(false);
        tutorial.SetActive(false);
        score.SetActive(false);
        textBalloon.SetActive(false);
        if (message != 0)
        {
            textBalloon.SetActive(true);
        }
        if (message == 1)
        {
            intro.SetActive(true);
        } else if (message == 2)
        {
            score.SetActive(true);
        } else if (message == 3)
        {
            tutorial.SetActive(true);
        }

        if (started)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(worldPoint.x, transform.position.y);

            if (rb.gravityScale < 0 && transform.position.y >= 4f)
            {
                ResetLevel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            scoreSound.Play();
            Fish theFish = collision.GetComponent<Fish>();
            currentScore += theFish.score;
            theFish.Catch(currentScore);
        }
        else if (collision.tag == "Trash")
        {
            gameOverSound.Play();
            ResetLevel();
        }
        else if (collision.tag == "SeaBottom" && rb.gravityScale > 0)
        {
            reverseSound.Play();
            rb.velocity = Vector2.zero;
            rb.gravityScale *= -1;
            rb.drag = 0.7f;
            rb.angularDrag = 0.7f;
        }
    }

    void ResetLevel()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = Mathf.Abs(rb.gravityScale);
        rb.drag = 1f;
        rb.angularDrag = 1f;
        DestroyAll("Fish");
        DestroyAll("Trash");
        message = 1;
        if (highScore < currentScore)
        {
            highScore = currentScore;
            textScore.text = "Your best score is " + highScore.ToString() + "! Click to play again!";
        }
        currentScore = 0;
        transform.position = startPosition;
        started = false;
    }

    void DestroyAll(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }
}
