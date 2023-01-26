using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] GameObject _attack;
    [SerializeField] GameObject _attackOrigin;
    [SerializeField] AudioSource _magicSound;

    public float _attackForce = 30;

    GameObject _enemy;
    bool enemyIsDead = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("entered trigger");
            _enemy = other.gameObject;

            InvokeRepeating("FireRepeat", 0.5f, 2f);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _enemy)
            CancelInvoke("FireRepeat");
    }

    private void Update()
    {
        if (_enemy != null)
            enemyIsDead = _enemy.GetComponent<Enemy>().IsDead;

        if (enemyIsDead)
            CancelInvoke("FireRepeat");
    }

    void FireRepeat()
    {
        Debug.Log("fire repeat");
        /*
        int layerMask = 1 << 6;
        RaycastHit hit;
        // Does the ray intersect any objects in the enemies layer
        Vector3 position = transform.position + new Vector3(0, 1, 0);
        Vector3 _enemyPosition = _enemy.transform.position + new Vector3(0, 1, 0);
        Vector3 direction = _enemyPosition - position;

        if (Physics.Raycast(position, transform.TransformDirection(direction), out hit, 10, layerMask))
        {
            Debug.Log("raycast hit");
            Debug.DrawRay(position, transform.TransformDirection(direction) * hit.distance, Color.yellow);
        */
            GetComponentInParent<Animator>().SetTrigger("Cast");
            _magicSound.Play();
            Invoke("Fire", 0f);
            Invoke("Fire", 0.1f);
            Invoke("Fire", 0.2f);
        //}     
    }

    void Fire()
    {
        Debug.Log("firing");
        GameObject attackObject = Instantiate(_attack, _attackOrigin.transform.position, _attackOrigin.transform.rotation);
        gameObject.GetComponentInParent<Transform>().LookAt(_enemy.transform);
        Vector3 direction = _enemy.transform.position - _attackOrigin.transform.position;
        attackObject.GetComponent<Rigidbody>().AddForce(direction.normalized * _attackForce, ForceMode.Impulse);
    }
}
