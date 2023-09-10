using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CommandManager : MonoBehaviour
{
    private AttackCommand _command = AttackCommand.Normal;
    public bool isNormalAttack;
    public bool isComboAttack;
    public bool isDrag = false;
    private bool _isAttack;
    private bool _isSelect;
    public List<int> selectAttackList = new List<int>();
    public List<int> selectComboList = new List<int>();
    public Linerenderer linerenderer;

    public List<BattleIcon> battleIcons = new List<BattleIcon>();

    public async UniTask<AttackCommand> GetAttackCommand()
    {
        while (true)
        {
            if (_isAttack)
            {
                _isAttack = false;
                _isSelect = false;
                return _command;
            }
            await UniTask.DelayFrame(1);
        }
        
    }


    public void  DecideCommand()
    {
        if (_isSelect && selectAttackList.Count != 0)
        {
            _isAttack = true;
            InvokCancel();
        }  
    }

    public async void InvokCancel()
    {
        await UniTask.DelayFrame(1);
        foreach (BattleIcon battleIcon in battleIcons)
        {
            if (battleIcon.isSelect)
            {
                linerenderer.ClrearLine();
                battleIcon.CancelSelect();
            }


        }
    }

    public void ResetList()
    {
        selectAttackList = new List<int>();
    }
    public void NormalAttackSelect()
    {
        _command = AttackCommand.Normal;
        isNormalAttack = true;
        isComboAttack = false;
        _isSelect = true;
    }
    public void ComboAttackSecelt()
    {
        if (isDrag)
        {
            _command = AttackCommand.Combination;
            isNormalAttack = false;
            isComboAttack = true;
            _isSelect = true;
        }
    }
    public void CancelComand()
    {
        _isSelect = false;
    }
    public void Drag()
    {
        isDrag = true;
    }
    public void DragEnd()
    {
        isDrag = false;
    }
    public void AddList(int num)
    {
        selectAttackList.Add(num);
    }
    public void RemoveList(int num)
    {
        selectAttackList.Remove(num);
        linerenderer.ClrearLine();
    }
    public void SelectReset(BattleIcon battleIcon)
    {
        foreach(BattleIcon bc in battleIcons)
        {
            if(bc != battleIcon)
            {
                bc.CancelSelect();
                
            }
        }
    }
}
