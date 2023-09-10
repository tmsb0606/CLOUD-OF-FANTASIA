using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.Playables;

public class CutInManager : MonoBehaviour
{
    public GameObject cutinPrefab;
    private GameObject _Cutin;
    public CommandManager commandManager;

    // Start is called before the first frame update
    void Start()
    {
        _Cutin = Instantiate(cutinPrefab, transform.position, Quaternion.identity);
        _Cutin.SetActive(false);
    }

    public async UniTask ShowCutInUITask()
    {
        var children = _Cutin.transform.GetChild(1).gameObject;
        var children2 = _Cutin.transform.GetChild(2).gameObject;
        print(CardDataList.cardDatas[commandManager.selectAttackList[1]].name);
        print(CardDataList.cardDatas[commandManager.selectAttackList[0]].name);
        
        children.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Cutin_" + CardDataList.cardDatas[commandManager.selectAttackList[0]].name);
        children2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Cutin_" + CardDataList.cardDatas[commandManager.selectAttackList[1]].name);
        PlayableDirector playableDirector = _Cutin.GetComponent<PlayableDirector>();
        _Cutin.SetActive(true);
        playableDirector.Play();
        //animationが終わりそうな適当な時間
        await UniTask.Delay((int)(playableDirector.duration * 1000));
        return;
    }
}
