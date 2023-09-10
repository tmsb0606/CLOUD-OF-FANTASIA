using UnityEngine;
using UnityEngine.UI;

public class BattleIcon : MonoBehaviour
{
    public int iconNo;

    public bool isTouch { get; private set; } = false;
    public bool isRelease { get; private set; } = false;

    private Sprite _charSprite;
    private Image _image;

    private CommandManager _commandManager;
    private BattleManager _battleManager;
    public bool isSelect = false;
    private bool isStartDrag = false;
    private void Start()
    {
        _image = GetComponent<Image>();
        _commandManager = GameObject.Find("CommandManager").GetComponent<CommandManager>();
        _battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    public void OnTouchAct()
    {
        Debug.Log("タッチされました");
        isTouch = true;
    }

    public void OnReleaseAct()
    {
        Debug.Log("指を離しました");
        isRelease = true;
    }

    public void TouchOff()
    {
        Debug.Log("キャンセル");
        isTouch = false;
    }

    public void ReleaseOff()
    {
        isRelease = false;
    }
    public void Select()
    {

        if (isSelect)
        {

            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            _commandManager.RemoveList(iconNo);
            isSelect = false;
        }
        else if(!isSelect&&_battleManager.gameState == BattleManager.GameState.Player)
        {
            if (_commandManager.selectAttackList.Count >= 2)
            {
                return;
            }
            this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            _commandManager.AddList(iconNo);
            isSelect = true;
        }

    }
    public void DragSelect()
    {
        if (!isSelect)
        {
            isStartDrag = false;
            _commandManager.AddList(iconNo);
        }
        this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        isSelect = true;
        
    }
    public void StartDrag()
    {
        isStartDrag = false;
        _commandManager.SelectReset(this);
        DragSelect();
        isStartDrag = true;
    }
    public void BetweenDrag()
    {
        if (_commandManager.isDrag)
        {
            DragSelect();
        }
    }
    /// <summary>
    /// コンボattack三人以上なら要らない。
    /// </summary>
    public void DragOut()
    {
        if (!isStartDrag&&_commandManager.isDrag)
        {
            CancelSelect();
        }
        
        
    }
    public void EndDrag()
    {
        if(_commandManager.selectAttackList.Count < 2)
        {
            CancelSelect();
            _commandManager.CancelComand();
        }
    }
    public void CancelSelect()
    {
        _commandManager.RemoveList(iconNo);
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        isSelect = false;
    }
}
