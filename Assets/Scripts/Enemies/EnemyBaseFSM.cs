using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseFSM : StateMachineBehaviour
{
    protected GameObject _enemy;
    protected GameObject _player;
    protected float _speed = 2f;
    protected float _rotationSpeed = 1f;
    protected float _accuracy = 2f;
    protected GameObject[] _waypoints;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.gameObject;
        _player = _enemy.GetComponent<EnemyAI>().GetPlayer();

        _waypoints = _enemy.GetComponent<EnemyAI>().WayPoints;
    }
}
