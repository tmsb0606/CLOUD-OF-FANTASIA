using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TurnUIManager : MonoBehaviour
{
    public GameObject turnUI;
    public Sprite yourTurn;
    public Sprite enemyTurn;

    private GameObject _turnUI;

    private void Start()
    {
        _turnUI = Instantiate(turnUI, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// valueが0ならマイターン、1ならエネミーターン
    /// </summary>
    /// <param name="value"></param>
    public void ShowTurnUI(int value)
    {
        var children = turnUI.transform.GetChild(0).gameObject;
        switch (value)
        {
            case 0:
                children.GetComponent<Image>().sprite = yourTurn;
                break;
            case 1:
                children.GetComponent<Image>().sprite = enemyTurn;
                break;
        }
        PlayableDirector playableDirector = _turnUI.GetComponent<PlayableDirector>();
        _turnUI.SetActive(true);
        playableDirector.Play();
    }

    /// <summary>
    /// ターンUI表示待ちが必要な時のために一応作った。
    /// UniTaskに触ったことがないので動くか分からない。
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public async UniTask ShowTurnUITask(int value)
    {
        var children = _turnUI.transform.GetChild(0).gameObject;
        switch (value)
        {
            case 0:
                children.GetComponent<Image>().sprite = yourTurn;
                break;
            case 1:
                children.GetComponent<Image>().sprite = enemyTurn;
                break;

        }
        PlayableDirector playableDirector = _turnUI.GetComponent<PlayableDirector>();
        _turnUI.SetActive(true);
        playableDirector.Play();
        //animationが終わりそうな適当な時間
        await UniTask.Delay((int)(playableDirector.duration * 1000));
        return;
    }
}
