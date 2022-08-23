using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Entity
{
    [SerializeField] private LayerMask _targetLayer;

    private Entity _targetEntity;
    private NavMeshAgent _navMeshAgent;

    [SerializeField] private ParticleSystem _hitEffect;

    private Animator _anim;
    private Renderer _monsterRenderer;

    [SerializeField] private float _damage = 20f;
    [SerializeField] private float _timeBetAttack = 0.5f;
    [SerializeField] private float _lastAttackTime;

    private bool HasTarget
    {
        get
        {
            if(_targetEntity != null && !_targetEntity.Dead)
            {
                return true;
            }

            return false;
        }
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _monsterRenderer = GetComponentInChildren<Renderer>();
    }
}
