using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUIManager : MonoBehaviour
{
    public GameObject damageUI;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowTest()
    {
        ShowDamageUI(100, this.transform);
    }

    public void ShowDamageUI(int damage,Transform transform)
    {
        //Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GameObject gameObject=  Instantiate(damageUI,canvas.transform);
        int randX = Random.Range(-10, 10);
        int randY = Random.Range(-10, 10);
        gameObject.transform.position = new Vector3(transform.position.x+randX, transform.position.y+randY, transform.position.z);
        //gameObject.transform.position = transform.position;
        gameObject.GetComponent<TextMeshProUGUI>().text = damage.ToString();
        
    }

    public void ShowHealUI(int damage, Transform transform)
    {

        //Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GameObject gameObject = Instantiate(damageUI, canvas.transform);
        int randX = Random.Range(-10, 10);
        int randY = Random.Range(-10, 10);
        //gameObject.transform.position = new Vector3(transform.position.x + randX, transform.position.y + randY, transform.position.z);
        gameObject.transform.position = transform.position;
        gameObject.GetComponent<TextMeshProUGUI>().text = damage.ToString();

        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
        Color color = text.color;
        color.r =0f;
        color.g = 255f;
        text.color = color;

    }
}
