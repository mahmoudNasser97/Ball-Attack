using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SmallBall"))
        {
            Destroy(other.gameObject);
            transform.localScale += new Vector3(5f, 5f, 5f);
            rb.mass += 0.5f;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBall"))
        {
            if (transform.localScale.magnitude > collision.transform.localScale.magnitude)
            {
                Destroy(collision.gameObject);
                transform.localScale += new Vector3(10f, 10f, 10f);
                rb.mass += 0.5f;
            }
            else
            {

            }
        }
    }
}
