using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    public Rigidbody ballRigidBody;
    public float impulseForce = 5f;
    private Vector3 startPosition;
    public int perfectPass = 0;
    public bool isSuperSpeedActive;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
        {
            return;
        }

        if (isSuperSpeedActive)
        {
            if (!collision.transform.GetComponent<Goal>())
            {
                Destroy(collision.transform.parent.gameObject, 0.3f);
            }
        }
        else
        {
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                deathPart.HitDeathPart();
            }
        }

        

        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.AddForce(Vector3.up * impulseForce,ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    void Update()
    {
        if (perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            ballRigidBody.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }

    private void AllowCollision()
    {
        ignoreNextCollision = !ignoreNextCollision;
    }

    public void ResetBall()
    {
        transform.position = startPosition;
    }
}
