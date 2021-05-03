using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour
{

    [SerializeField]
    private float speed = 5;

    [SerializeField] private Transform firstPoint = null;
    [SerializeField] private Transform secondPoint = null;

    private bool resetDirection = false;

    private Rigidbody rb;

    private Vector3 direction;
    private Vector3 orderPosition;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();

        StartCoroutine(patrolMove());

    }

    IEnumerator patrolMove()
    {

        if (resetDirection == true)
        {
            orderPosition = firstPoint.position;
        }
        else
        {
            orderPosition = secondPoint.position;
        }

        resetDirection = !resetDirection;

        direction = orderPosition - transform.position;

        transform.forward = direction;
        rb.velocity = transform.forward * speed;

        float distance = Vector3.Distance(orderPosition, transform.position);

        float calculateTimeForStop = distance / speed;

        yield return new WaitForSeconds(calculateTimeForStop);

        StartCoroutine(patrolMove());

    }


    private void stopMove()
    {

        rb.velocity = Vector3.zero;

    }
}
