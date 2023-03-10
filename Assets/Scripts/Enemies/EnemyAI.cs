using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _attack;
    [SerializeField] GameObject _attackOrigin;
    [SerializeField] GameObject _arrow;
    [SerializeField] AudioSource _arrowSound;
    [SerializeField] Health _playerHealth;
    [SerializeField] GameObject[] _waypoints;
    [SerializeField] GameObject _fieldOfView;

    public GameObject[] WayPoints => _waypoints;

    Animator _anim;

    bool _shouldTryToFindPlayer = true;
    bool _playerIsInSight = false;

    Vector3 _playerPos;
    Vector3 _enemyPos;
    Vector3 _rayDirection;

    void Start() => _anim = GetComponent<Animator>();

    void Update()
    {
        _fieldOfView.GetComponent<FieldOfView>().SetAimDirection(Vector3.forward);


        if (_shouldTryToFindPlayer)
        {
            float distanceFromPlayer = Vector3.Distance(transform.position, _player.transform.position);
            float angleFromPlayer = Vector3.Angle(_player.transform.position - transform.position, transform.forward);

            _anim.SetFloat("distance", distanceFromPlayer);
            _anim.SetFloat("angle", angleFromPlayer);

            _playerPos = _player.transform.position + new Vector3(0, 1, 0);
            _enemyPos = transform.position + new Vector3(0, 1, 0);
            _rayDirection = _playerPos - _enemyPos;

            RaycastHit hit;

            if (Physics.Raycast(_enemyPos, _rayDirection, out hit))
            {
                if (hit.transform == _player.transform)
                    _playerIsInSight = true;               
                else
                    _playerIsInSight = false;
            }

            _anim.SetBool("playerInSight", _playerIsInSight);

            if (distanceFromPlayer <= 8 && angleFromPlayer <= 35 && _playerIsInSight)
            {
                _fieldOfView.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 0.2f);

            } else
            {
                _fieldOfView.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
            }
        }        
    }

    public GameObject GetPlayer() => _player;

    void Fire()
    {
        if (_playerHealth.HealthPoints > 0)
        {
            GameObject b = Instantiate(_attack, _attackOrigin.transform.position, _attackOrigin.transform.rotation);
            b.GetComponent<Rigidbody>().AddForce(_attackOrigin.transform.forward * 800);
            _arrowSound.Play();
        }  
    }
    public void StartFiring() => InvokeRepeating("Fire", 0.5f, 0.5f);

    public void StopFiring() => CancelInvoke("Fire");

    void MakeArrowVisible() => _arrow.GetComponent<MeshRenderer>().enabled = true;

    void MakeArrowInvisible() => _arrow.GetComponent<MeshRenderer>().enabled = false;

    public void StopChase()
    {
        _shouldTryToFindPlayer = false;
        _anim.SetFloat("distance", 20);
        _anim.SetBool("playerInSight", false);
    }
}
