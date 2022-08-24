using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Entity
{
    [SerializeField] private LayerMask _targetLayer;

    private Entity _targetEntity;
    private NavMeshAgent _navMeshAgent;
    private Animator _anim;

    [SerializeField] private float _damage = 20f;

    private bool _isAttack = false;

    private bool HasTarget
    {
        get
        {
            if(_targetEntity != null && !_targetEntity._dead)
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
    }

    public void SetUp(MonsterData monsterData)
    {
        _startHealth = monsterData.health;
        _damage = monsterData.damage;
        _navMeshAgent.speed = monsterData.speed;
        transform.localScale = monsterData.size;

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        while (!_dead)
        {
            if (!_isAttack)
            {
                if (HasTarget)
                {
                    _navMeshAgent.isStopped = false;
                    _navMeshAgent.SetDestination(_targetEntity.transform.position);
                }
                else
                {
                    _navMeshAgent.isStopped = true;

                    Collider[] colliders = Physics.OverlapSphere(transform.position, 100f, _targetLayer);

                    for(int i = 0; i < colliders.Length; i++)
                    {
                        Entity entity = colliders[i].GetComponent<Entity>();

                        if(entity != null && !entity._dead)
                        {
                            _targetEntity = entity;
                            break;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        _navMeshAgent.isStopped = true;

        _anim.SetTrigger("IsDie");
    }

    private void OnTriggerStay(Collider other)
    {
        if(!_dead)
        {
            Entity attackTarget = other.GetComponent<Entity>();

            if(attackTarget != null && attackTarget == _targetEntity && !attackTarget._dead && !_isAttack)
            {
                _isAttack = true;

                _anim.SetTrigger("IsAttack");
                _navMeshAgent.isStopped = true;

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                attackTarget.OnDamage(_damage, hitPoint, hitNormal);
            }
        }
    }

    public void AttackFalse()
    {
        _isAttack = false;
    }

    public void PushEvent()
    {
        PoolManager.Instance.Push(this);
    }
}
