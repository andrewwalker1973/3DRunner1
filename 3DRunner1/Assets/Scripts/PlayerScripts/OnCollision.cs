using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
     //public PlayerMotor m_char;
    public CharacterPlayer m_char;



    private void Start()
    {
        m_char = FindObjectOfType<CharacterPlayer>();
    }

    public void UpdateCharCollider()
    {
        m_char = FindObjectOfType<CharacterPlayer>();
    }

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
