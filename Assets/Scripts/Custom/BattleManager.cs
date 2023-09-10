using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Net.NetworkInformation;
using UnityEngine.Playables;
using UnityEditor;

public class BattleManager : MonoBehaviour
{
    public enum GameState
    {
        Player,
        Enemy
    }
    // �v���C���[�̊Ǘ�
    [SerializeField] private PlayerManager _playerManager;
    // �G�̊Ǘ�
    [SerializeField] private EnemyManager _enemyManager;
    // �o�g���A�C�R��
    [SerializeField] private BattleIcon[] _battleIcons;
    // �A�C�R���̃A�j���[�V����
    [SerializeField] private PlayableDirector[] _iconDirector;
    // �G�̃A�j���[�V����
    [SerializeField] private PlayableDirector _enemyDirector;
    // �^�[���\��
    [SerializeField] private TurnUIManager _turnUIManager;
    // ���U���g�\��
    [SerializeField] private Result _result;
    // �G�I�u�W�F�N�g
    [SerializeField] private GameObject _enemyObject;
    // �G�t�F�N�g
    [SerializeField] private EffectManager _effectManager;
    // SE
    [SerializeField] private IndexSE _indexSE;

    [SerializeField] private CommandManager _commandManager;
    [SerializeField] private CutInManager _cutinManager;
    [SerializeField] private DamageUIManager _damageUIManager;

    private AttackCommand _command = AttackCommand.Normal;

    public GameState gameState = GameState.Player;
 
    private void Start()
    {
        // �o�g������������΂�
        PlayerAdvanceBattle();
    }

    /// <summary>
    /// �v���C���[��s�o�g������
    /// </summary>
    private async void PlayerAdvanceBattle()
    {
        Debug.Log("�o�g���X�^�[�g");
        while (true)
        {
            await _turnUIManager.ShowTurnUITask(0);

            Debug.Log("�v���C���[�̃^�[��");
            // �v���C���[�̃^�[��
            gameState = GameState.Player;
            _command = await _commandManager.GetAttackCommand();
            int[] selectChar = _commandManager.selectAttackList.ToArray();
            if(_command == AttackCommand.Combination)
            {
                await _cutinManager.ShowCutInUITask();
            }
            int playerAtk = PlayerAttack(_command, selectChar);
            print(playerAtk);
            if(playerAtk <= 0)
            {
                _damageUIManager.ShowHealUI(playerAtk*-1, _playerManager.player.transform);
                await UniTask.Delay(100);
            }
            else
            {
                _indexSE.PlaySoundEffect(Random.Range(0, 1));
                foreach (int i in selectChar)
                {
                    _iconDirector[i].Play();
                }
                await UniTask.Delay((int)(_iconDirector[0].duration * 1000));
                _effectManager.ShowEffect(_enemyObject);
                // �G�̃_���[�W�A�j���[�V�������Đ�
                // �A�j���[�V�����̒�����await����
                _enemyManager.Damage(playerAtk);
                _damageUIManager.ShowDamageUI(playerAtk,_enemyManager.Enemy.transform);

            }
            _commandManager.ResetList();
            // �I�����ꂽ�L�����̍U���A�j���[�V�������Đ�
            Debug.Log(selectChar.Length);


            // ���s����
            if (HPCheck())
            {
                break;
            }


            // �G�̃^�[���ƃ^�C�����C�����g���ăA�j���[�V������\��
            // �A�j���[�V�����̒�����await
            gameState = GameState.Enemy;
            await _turnUIManager.ShowTurnUITask(1);

            int enemyAtk = _enemyManager.NormalAttack();
            _enemyDirector.Play();
            await UniTask.Delay((int)(_enemyDirector.duration * 1000));

            // �v���C���[�̃_���[�W�A�j���[�V�������Đ�
            // �A�j���[�V�����̒�����await
            _playerManager.Damage(enemyAtk);

            // ���s����
            if (HPCheck())
            {
                break;
            }

            Debug.Log($"{_playerManager.hp},{_enemyManager.hp}");

            await UniTask.DelayFrame(1);
        }
        BattleFinish();
    }

    /// <summary>
    /// �G��s�o�g������
    /// </summary>
    private async void EnemyAdvanceBattle()
    {
        while (true)
        {
            // �G�̃^�[��
            int enemyDmg = _enemyManager.NormalAttack();
            // ���s����
            if (HPCheck())
            {
                break;
            }
            await Task.Delay(0);

            // �v���C���[�̃^�[��
            // TODO(����) 0�ł͂Ȃ�PlayerAttack()�ɏ���������
            int playerDmg = 0;
            // ���s����
            if (HPCheck())
            {
                break;
            }
        }
        BattleFinish();
    }

    /// <summary>
    /// �v���C���[���̍U��
    /// </summary>
    /// <param name="command"> �U�����@ </param>
    /// <param name="cardNo"> �I�����ꂽ�J�[�h�i���o�[ </param>
    /// <returns> �_���[�W��Ԃ� </returns>
    /// (cardNo�Ɋi�[����閇���͍��̂Ƃ���ő�񖇁ACombination�U���̎��͐�Γ񖇂ɂȂ�͂��ł�)
    private int PlayerAttack(AttackCommand command, int[] cardNo)
    {
        int damage = 0;

        switch (command)
        {
            case AttackCommand.Normal:
                for(int i = 0; i < cardNo.Length; i++)
                {
                    damage += _playerManager.NormalAttack(cardNo[i]);
                }
                break;

            case AttackCommand.Combination:
                // TODO(����) 0�ł͂Ȃ��v���C���[���J�[�h�̏������g��
                print("�R���r�A�^�b�N");
                
                damage += _playerManager.ComboSkil(cardNo, _enemyManager);
                break;
        }

        return damage;
    }

    private int EnemyAttack(AttackCommand command, int[] cardNo)
    {
        // TODO(����) �G�̍U�����@���܂������ĂȂ�
        int damage = 0;
        return damage;
    }

    /// <summary>
    /// �v���C���[�A�܂��͓G�A�ǂ��炩��HP�������Ȃ��true��Ԃ�
    /// </summary>
    private bool HPCheck()
    {
        if (_enemyManager.hp < 0)
        {
            return true;
        }

        if (_playerManager.hp < 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// ���s��\������
    /// </summary>
    private void BattleFinish()
    {
        if (_playerManager.hp <= 0)
        {
            Debug.Log("�G�̏���");
            _result.ShowResult(false);
            return;
        }
        else
        {
            Debug.Log("�v���C���[�̏���");
            _result.ShowResult(true);
        }
    }

    private async UniTask<int[]> SelectCommand()
    {
        List<int> selectChara = new List<int>();
        while(true)
        {
            foreach(BattleIcon icon in _battleIcons)
            {
                if(icon.isTouch)
                {
                    selectChara.Add(icon.iconNo);
                    icon.TouchOff();
                }

                if (selectChara.Count >= 2)
                {
                    return selectChara.ToArray();
                }
            }
            await UniTask.DelayFrame(1);
        }
    }

    public void ToggleCommand()
    {
        _command++;
        
        if (_command > AttackCommand.Combination)
        {
            _command = AttackCommand.Normal;
        }
    }
}
