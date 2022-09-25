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
        if (collision.gameObject.CompareTag("Blockage") || collision.gameObject.CompareTag("Green Light"))
        {
            if (collision.gameObject.CompareTag("Blockage") && count == 0)
            {
                agent.isStopped = true;
                animator.SetFloat("Vertical", 0);
            }
        }

        if (collision.gameObject.CompareTag("Car"))
        {
            //Debug.Log("Car");
            agent.enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(this.gameObject, 4f);
            animator.Play("Die");
            GameController.deadPedestrian++;
        }       
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Blockage") || collision.gameObject.CompareTag("Green Light"))
        {
            count++;
            if (count == 2)
            {
                count = 0;
            }
           
        }
        
    }
    void UpdateDestination()
    {
        target = GameController.instance.targets[waypointIndex].position;
        agent.SetDestination(target);
        animator.SetFloat("Vertical", !agent.isStopped ? 1 : 0);
    }

    void IterateWaypointIndex()
    {
        waypointIndex = Random.Range(0, GameController.instance.targets.Length);
    }
}
