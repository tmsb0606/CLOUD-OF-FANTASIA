using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linerenderer : MonoBehaviour
{
    private LineRenderer _linerend;
    public Vector3 _startPos;
    public Vector3 _endPos;
    public bool isClick;
    private bool _isMove;
    // Start is called before the first frame update
    void Start()
    {
        _linerend = GetComponent<LineRenderer>();
        _linerend.startWidth = 0.2f;
        _linerend.endWidth = 0.2f;

    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)
        {
            //print(_endPos);
            if (_isMove)
            {
                MoveEndPos();
            }
            _linerend.SetPosition(0, _startPos);
            _linerend.SetPosition(1, _endPos);
        }
        else
        {
            if(_linerend.enabled == true)
            {
                ClrearLine();
            }
        }
    }

    private void MoveEndPos()
    {
        DrawLine();
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        _endPos = Camera.main.ScreenToWorldPoint(mousePosition);
    }
    public void StartClick()
    {
        this.isClick = true;
        _isMove = true;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        _startPos = Camera.main.ScreenToWorldPoint(mousePosition);
        DrawLine();
    }
    public void EndClick()
    {
        _isMove = false;
    }
    public void MidleClick()
    {
        //中間点のスクリプトを書く。
    }
    //消すときに使う。
    public void ClrearLine()
    {
        _linerend.enabled = false;
    }
    //表示するときに使う。
    public void DrawLine()
    {
        _linerend.enabled = true;
    }
}
