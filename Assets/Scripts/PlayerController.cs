using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 40f;
    public int health = 5;
    private Rigidbody rb;

    private int score = 0;

    private float horizontal;
    private float vertical;

   public TextMeshProUGUI scoreText;

    public TextMeshProUGUI healthText;

    public TextMeshProUGUI winLoseText;

    public UnityEngine.UI.Image winLoseBG;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }

        if (Time.frameCount % 10 == 0)
        {
            if (health <= 0)
            {   
                GameOver();
                Debug.Log("Game Over!");
               // health = 5;
              //  score = 0;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void FixedUpdate()
    {
        KeyMovements();
    }


    public void KeyMovements()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rb.AddForce(new Vector3(horizontal, 0, vertical) * (speed * Time.deltaTime), ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            Debug.Log("score : " + score);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
            Debug.Log("Health: " + health);
        }

        if (other.CompareTag("Goal"))
        {
            Debug.Log("You win!");
            WinGame();
        }
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    private void WinGame()
    {
        winLoseText.color = Color.black;
        winLoseBG.color = Color.green;
        winLoseText.text = "You win";

    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        winLoseText.color = Color.white;
        winLoseBG.color = Color.red;
        winLoseText.text = "Game Over!";
        StartCoroutine(LoadScene(3f));
    }

    private IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
