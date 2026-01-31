using System.Data.Common;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Obstacle")
        {
            Debug.Log("The obstacle touched the Player!");
        }

        Debug.Log("touched");
    }
}
