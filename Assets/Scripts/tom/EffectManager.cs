using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EffectManager : MonoBehaviour
{
    public GameObject effectObject;
    public GameObject canvas;
    private GameObject effect;
    //public GameObject ta
    private void Start()
    {
        effect = Instantiate(effectObject, canvas.transform);
        effect.SetActive(false);
    }
    /// <param name="taget"></param>
    public async void ShowEffect(GameObject target)
    {
        effect.SetActive(true);
        effect.transform.position = target.transform.position;
        await UniTask.DelayFrame(50);
        effect.SetActive(false);
    }
}
