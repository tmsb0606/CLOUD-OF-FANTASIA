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
    // プレイヤーの管理
    [SerializeField] private PlayerManager _playerManager;
    // 敵の管理
    [SerializeField] private EnemyManager _enemyManager;
    // バトルアイコン
    [SerializeField] private BattleIcon[] _battleIcons;
    // アイコンのアニメーション
    [SerializeField] private PlayableDirector[] _iconDirector;
    // 敵のアニメーション
    [SerializeField] private PlayableDirector _enemyDirector;
    // ターン表示
    [SerializeField] private TurnUIManager _turnUIManager;
    // リザルト表示
    [SerializeField] private Result _result;
    // 敵オブジェクト
    [SerializeField] private GameObject _enemyObject;
    // エフェクト
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
        // バトル処理を一回飛ばす
        PlayerAdvanceBattle();
    }

    /// <summary>
    /// プレイヤー先行バトル処理
    /// </summary>
    private async void PlayerAdvanceBattle()
    {
        Debug.Log("バトルスタート");
        while (true)
        {
            await _turnUIManager.ShowTurnUITask(0);

            Debug.Log("プレイヤーのターン");
            // プレイヤーのターン
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
                // 敵のダメージアニメーションを再生
                // アニメーションの長さ分awaitする
                _enemyManager.Damage(playerAtk);
                _damageUIManager.ShowDamageUI(playerAtk,_enemyManager.Enemy.transform);

            }
            _commandManager.ResetList();
            // 選択されたキャラの攻撃アニメーションを再生
            Debug.Log(selectChar.Length);


            // 勝敗判定
            if (HPCheck())
            {
                break;
            }


            // 敵のターンとタイムラインを使ってアニメーションを表示
            // アニメーションの長さ分await
            gameState = GameState.Enemy;
            await _turnUIManager.ShowTurnUITask(1);

            int enemyAtk = _enemyManager.NormalAttack();
            _enemyDirector.Play();
            await UniTask.Delay((int)(_enemyDirector.duration * 1000));

            // プレイヤーのダメージアニメーションを再生
            // アニメーションの長さ分await
            _playerManager.Damage(enemyAtk);

            // 勝敗判定
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
    /// 敵先行バトル処理
    /// </summary>
    private async void EnemyAdvanceBattle()
    {
        while (true)
        {
            // 敵のターン
            int enemyDmg = _enemyManager.NormalAttack();
            // 勝敗判定
            if (HPCheck())
            {
                break;
            }
            await Task.Delay(0);

            // プレイヤーのターン
            // TODO(高武) 0ではなくPlayerAttack()に書き換える
            int playerDmg = 0;
            // 勝敗判定
            if (HPCheck())
            {
                break;
            }
        }
        BattleFinish();
    }

    /// <summary>
    /// プレイヤー側の攻撃
    /// </summary>
    /// <param name="command"> 攻撃方法 </param>
    /// <param name="cardNo"> 選択されたカードナンバー </param>
    /// <returns> ダメージを返す </returns>
    /// (cardNoに格納される枚数は今のところ最大二枚、Combination攻撃の時は絶対二枚になるはずです)
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
                // TODO(高武) 0ではなくプレイヤー側カードの処理を使う
                print("コンビアタック");
                
                damage += _playerManager.ComboSkil(cardNo, _enemyManager);
                break;
        }

        return damage;
    }

    private int EnemyAttack(AttackCommand command, int[] cardNo)
    {
        // TODO(高武) 敵の攻撃方法をまだ聞けてない
        int damage = 0;
        return damage;
    }

    /// <summary>
    /// プレイヤー、または敵、どちらかのHPが無くなればtrueを返す
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
    /// 勝敗を表示する
    /// </summary>
    private void BattleFinish()
    {
        if (_playerManager.hp <= 0)
        {
            Debug.Log("敵の勝ち");
            _result.ShowResult(false);
            return;
        }
        else
        {
            Debug.Log("プレイヤーの勝ち");
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
