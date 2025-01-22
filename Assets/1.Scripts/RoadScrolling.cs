using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScrolling : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        MovePosition();
    }

    private void MovePosition()
    {
        if(transform.position.y <= -9f)
        {
            transform.position = new Vector3(0.1f, 11, 0);
        }
    }
}
