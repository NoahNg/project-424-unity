using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavController : MonoBehaviour
{
    NavMeshAgent agent;
    int waypointIndex;//the index of the waypoints
    Vector3 target;
    int count = 0;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = Random.Range(0, GameController.instance.targets.Length);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        UpdateDestination();//so we have somewhere to go when the game begins
    }

    // Update is called once per frame
    void Update()
    {
        //Pedestrians can go when all the lights are red
        if (BlockageController.canGo == true && agent.isStopped == true)
        {
            agent.isStopped = false;
            animator.SetFloat("Vertical", 1);
        }

        //check if the distance to our target is less than a certain amount, if it is then we will iterate our waypoints and update our destination
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //collide with a blockage
        if (collision.gameObject.CompareTag("Blockage") || collision.gameObject.CompareTag("Green Light"))
        {
            //if one the lights are green, and the AI is entering the intersection (count == 0)
            if (collision.gameObject.CompareTag("Blockage") && count == 0)
            {
                //then stop the AI
                agent.isStopped = true;
                animator.SetFloat("Vertical", 0);
            }
        }

        //die if being hit by cars
        if (collision.gameObject.CompareTag("Car"))
        {
            agent.enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(this.gameObject, 4f);
            animator.Play("Die");
            GameController.deadPedestrian++;
        }       
    }

    private void OnCollisionExit(Collision collision)
    {
        //this block of code determines whether the AI is entering the intersection, or leaving it
        //whenever the AI collide with a blockage, no matter the tag, count will go up by 1
        if (collision.gameObject.CompareTag("Blockage") || collision.gameObject.CompareTag("Green Light"))
        {
            count++;

            //if count == 1: entering the intersection
            //if count == 2: leaving the intersection 
            if (count == 2)
            {
                count = 0;
            }
           
        }
        
    }

    //set the destination for the pedestrians
    void UpdateDestination()
    {
        target = GameController.instance.targets[waypointIndex].position;
        agent.SetDestination(target);
        animator.SetFloat("Vertical", !agent.isStopped ? 1 : 0);
    }

    //set the next way point for the pedestrians to go to
    void IterateWaypointIndex()
    {
        waypointIndex = Random.Range(0, GameController.instance.targets.Length);
    }
}
