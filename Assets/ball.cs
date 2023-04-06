using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ball : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float speed;
    public bool resetHasBeenCalled;
    public bool gameHasStarted;
    public int leftPlayerScore = 0;
    public int rightPlayerScore = 0;
    public Vector2 velocity;
    public TextMeshProUGUI leftPlayerText;
    public TextMeshProUGUI rightPlayerText;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        gameHasStarted = true;
    }

    private void Update() //check if game started and wait for reset
    {
        if (gameHasStarted || resetHasBeenCalled)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ResetAndSetRandomVelocity();
            }
        }
    }

    private void ResetBall()
    {
        rb2D.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        resetHasBeenCalled = true;
    }

    private void ResetAndSetRandomVelocity()
    {
        ResetBall();
        rb2D.velocity = GenerateRandomVector2Without0(true) * speed;
        velocity = rb2D.velocity;
    }

    private Vector2 GenerateRandomVector2Without0(bool returnNormalized)
    {
        Vector2 newRandomVector = new Vector2();
        
        bool shouldXBeLessThanZero = Random.Range(0, 100) % 2 == 0;
        newRandomVector.x = shouldXBeLessThanZero ? Random.Range(-.8f, -.1f) : Random.Range(.1f, .8f);
        
        bool shouldYBeLessThanZero = Random.Range(0, 100) % 2 == 0;
        newRandomVector.y = shouldYBeLessThanZero ? Random.Range(-.8f, -.1f) : Random.Range(.1f, .8f);
        
        return returnNormalized ? newRandomVector.normalized : newRandomVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2D.velocity = Vector2.Reflect(velocity, collision.contacts[0].normal);
        velocity = rb2D.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.position.x > 0)
        {
            leftPlayerScore += 1;
            leftPlayerText.text = leftPlayerScore.ToString();
            Debug.Log("Left player: " + leftPlayerScore);
        }
        if(transform.position.x < 0)
        {
            rightPlayerScore += 1;
            rightPlayerText.text = rightPlayerScore.ToString();
            Debug.Log("Right player: " + rightPlayerScore);
        }
        ResetBall();
    }

}
