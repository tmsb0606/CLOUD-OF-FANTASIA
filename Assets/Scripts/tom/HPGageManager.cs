using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGageManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private EnemyManager _enemyManager;

    [SerializeField] private Image _playerHPGage;
    [SerializeField] private Image _enemyHPGage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerHPGage.fillAmount = (float)_playerManager.hp/ _playerManager.maxHP;
        _enemyHPGage.fillAmount = (float)_enemyManager.hp / _enemyManager.maxHP;
    }
}
