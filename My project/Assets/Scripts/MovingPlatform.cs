using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] movePos;
    [SerializeField] private float speed;
    private Transform nextPos;
    private int posIndex = 0;

    private void Awake()
    {
        foreach (GameObject child in movePos)
        {
            child.transform.parent = null;
        }
    }

    private void Start()
    {
        nextPos = movePos[posIndex].transform;
    }

    private void Update()
    {
        if (transform.position == nextPos.position)
        {
            posIndex++;

            if (posIndex == movePos.Length)
            {
                posIndex = 0;
            }

            nextPos = movePos[posIndex].transform;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
