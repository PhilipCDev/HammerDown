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
        }

        protected override void GoToTarget()
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition.position, step);
            float distance = Vector2.Distance (transform.position, _targetPosition.position);
            
            // TODO Change animation to idle/walking
            
            var _direction = (_targetPosition.position - transform.position).normalized;
            transform.right = _direction;
            transform.rotation *= Quaternion.Euler(90.0f, 0, 0);
        }
    }
}