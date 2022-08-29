using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Title : MonoBehaviour
{
    [SerializeField] private Image _howTo;

    public void GoStart()
    {
        SceneLoader.Instance.LoadScene("Main");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void HowTo(bool value)
    {
        if (value)
        {
            _howTo.rectTransform.DOAnchorPosY(0, 1f);
        }
        else
        {
            _howTo.rectTransform.DOAnchorPosY(-1080, 1f);
        }
    }
}
