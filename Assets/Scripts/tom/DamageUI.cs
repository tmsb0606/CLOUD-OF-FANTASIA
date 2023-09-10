using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class DamageUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RemoveUI();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
    private async void RemoveUI()
    {
        while (true)
        {
            
            TextMeshProUGUI text = this.GetComponent<TextMeshProUGUI>();
            Color color = text.color;
            color.a -= 0.05f;
            text.color = color;
            if (color.a <= 0.05)
            {
                break;
            }
            await UniTask.DelayFrame(1);
            //.a -= 0.1f;
        }
        this.gameObject.SetActive(false);
    }
}
