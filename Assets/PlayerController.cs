using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public static int lives = 3;
    public static int prizes = 3;
    public static int points = 0;
    public Text scoreText;
    public Text livesText;
    public Text prizeText;

    public Text gameOverText;

    Vector3 originalPos;

    void Start()
    {
        agent.updateRotation = false;
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }


    // Update is called once per frame
    void Update()
    {

        scoreText.text = "Score: " + points.ToString();
        livesText.text = "Lives: " + lives.ToString();
        prizeText.text = "Prizes Remaining: " + prizes.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity);
        }
        else
        {
            character.Move(Vector3.zero);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            lives--;
            //transform.position = originalPos;

            if (lives == 0)
            {
                Debug.Log("You Lose!");
                Time.timeScale = 0;
                gameOverText.text = "You Lose!";
            }
        }

        if (collision.collider.tag == "Prize")
        {
            Destroy(collision.gameObject);
            prizes--;
            points = points + 10;

            if(prizes == 0)
            {
                Debug.Log("You Win!");
                Time.timeScale = 0;
                gameOverText.text = "You Win!";
            }
        }
    }
}
