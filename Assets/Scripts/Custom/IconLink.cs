using UnityEngine;
public class IconLink : MonoBehaviour
{
    [SerializeField] private BattleIcon[] icons;

    private void Start()
    {
        Input.multiTouchEnabled = false;
    }

    private void Update()
    {
        if (Input.touchCount == 0)
        {

        }
    }
}
