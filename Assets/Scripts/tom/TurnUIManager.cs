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
    /// value��0�Ȃ�}�C�^�[���A1�Ȃ�G�l�~�[�^�[��
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
    /// �^�[��UI�\���҂����K�v�Ȏ��̂��߂Ɉꉞ������B
    /// UniTask�ɐG�������Ƃ��Ȃ��̂œ�����������Ȃ��B
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
        //animation���I��肻���ȓK���Ȏ���
        await UniTask.Delay((int)(playableDirector.duration * 1000));
        return;
    }
}
