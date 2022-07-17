using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeMaskSimple : DudeMaskBaseClass
{

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSpeed = 5;
        directionForMove = 1;
        dudeMaskIsAlive = true;
    }


    private void Update()
    {
        if (dudeMaskIsAlive)
        {
            Move();
            TurnBack();
        }
        AnimatorController();

    }

   
   




}
