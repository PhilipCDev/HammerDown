using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammerDown.Animals
{
    public class Spider : Animal
    {
        protected override void SetUp()
        {
            currentTarget = Targets.Hand;
            _targetPosition = GetTargetPosition(currentTarget);
            _animator = GetComponent<Animator>();
        }

        protected override void GoToTarget()
        {
            float step = speed * Time.deltaTime;
            
            float distance = Vector2.Distance (transform.position, _targetPosition.position);
            Debug.Log(distance);
            if (distance < 0.9f)
            {
                _animator.SetBool("isRunning", false);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition.position, step);
                _animator.SetBool("isRunning", true);
            }
            
            var _direction = (_targetPosition.position - transform.position).normalized;
            transform.right = _direction;
            transform.rotation *= Quaternion.Euler(90.0f, 0, 0);
        }
    }
}