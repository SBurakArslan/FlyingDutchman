using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    Rigidbody2D rb;
    Vector3 _playerStartPos;
    GameObject gameobject;
    bool IsGrounded;
    float maxYPosition = 4f;
    public ParticleSystem waterParticle;
    Animator animator;
    private ScoreManager scoreManager;
    public GameObject pause;
    private bool gameIsOver = false;
    public GameObject GameOverUI;


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag ("Ground")) {
            IsGrounded = true;
            animator.SetBool("IsGrounded", true);
            scoreManager = FindObjectOfType<ScoreManager>();
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            IsGrounded = false;
            animator.SetBool("IsGrounded", false);
        }
    }
    private void Start() {
        Time.timeScale = 1f;
        IsGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        _playerStartPos = gameObject.transform.position;
        animator = gameObject.GetComponent<Animator>();
        GameOverUI.SetActive(false);
    }
    private void Update() {

        if (Input.GetKey(KeyCode.Space)) {
            Vector3 currentPosition = transform.position;
            if (currentPosition.y < maxYPosition) {
                rb.velocity = new Vector2(0, 5f);
                if (!waterParticle.isPlaying) {
                    waterParticle.Play();
                }
            } else {
                rb.velocity = new Vector2(0, 0);
                currentPosition.y = maxYPosition;
                transform.position = currentPosition;
            }
        }
        if (IsGrounded) {
            if (waterParticle.isPlaying) {
                waterParticle.Stop();
            }
        } else {
            if (!waterParticle.isPlaying) {
                waterParticle.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pause.activeSelf) {
                Resume();
            } else {
                Pause();
            }
        }
        if (gameIsOver) {
            if (Input.GetKeyDown(KeyCode.E)) {
                RestartGame();
            }
        }
    }


    void RestartGame() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Pause() {
        pause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume() {
        pause.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Obstacles") || collision.gameObject.CompareTag("Cloud")) {
            scoreManager.CheckHighScore();
            gameIsOver = true;
            Time.timeScale = 0f;
            GameOverUI.SetActive(true);
        }
    }
}