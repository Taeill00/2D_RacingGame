using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if(transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }
}
