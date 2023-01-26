using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponChange : MonoBehaviour
{
    [SerializeField] List<GameObject> _playerWeapons = new List<GameObject>();
    [SerializeField] GameObject _meleeAttack;
    [SerializeField] GameObject _rangedAttack;

    public void ChangeWeapon(WeaponType equippedWeapon)
    {
        foreach (GameObject w in _playerWeapons)
        {
            if (w.name.Equals(equippedWeapon.typeName.ToString()))
                w.SetActive(true);
            else
                w.SetActive(false);
        }

        if (equippedWeapon.range.Equals(WeaponType.Range.Melee))
        {
            _meleeAttack.SetActive(true);
            _rangedAttack.SetActive(false);
        }
        else if (equippedWeapon.range.Equals(WeaponType.Range.Ranged))
        {
            _meleeAttack.SetActive(false);
            _rangedAttack.SetActive(true);
        }
    }
}
