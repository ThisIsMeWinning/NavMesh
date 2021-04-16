using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public ThirdPersonCharacter character;

    Vector3 currPos;

    void Start()
    {
        agent.updateRotation = false;
    }


    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity);
        }
        else
        {
            character.Move(Vector3.zero);
        }

        currPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            transform.position = currPos - new Vector3(0.7f,0,0.7f);

        }

    }
}
