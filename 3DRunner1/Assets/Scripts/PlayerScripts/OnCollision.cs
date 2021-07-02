using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    public PlayerMotor m_char;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Player")
        {
           
            return;
        }
        Debug.Log("Hit " + collision);
        m_char.OnCharacterColliderHit(collision.collider);
    }
}
