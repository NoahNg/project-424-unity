using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MininapController : MonoBehaviour
{
    public Transform player;

    //Drag the camera along with the player
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
