using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private GameObject _winAnimation;
    [SerializeField] private GameObject _loseAnimation;

    public void ShowResult(bool isWinPlayer)
    {
        GameObject animation;
        if (isWinPlayer)
        {
            animation = Instantiate(_winAnimation);
        }
        else
        {
            animation = Instantiate(_loseAnimation);
        }

        Destroy(animation, 10);
    }
}
