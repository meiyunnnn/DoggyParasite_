using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool facingLeft = true;
    public float moveSpeed = 2f;
    public float distance = 1f;
    public bool inRange = false;
    public Transform player;
    public float attackRange = 10f;
    bool playerDetected;
    
    private void Start()
    {
        
    }
    private void Update()
    {
        
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        
    }
}