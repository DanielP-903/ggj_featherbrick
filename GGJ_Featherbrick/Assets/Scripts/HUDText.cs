using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDText : MonoBehaviour
{
    public Text score1, score2, score3, score4;
    public Text timer;
    private int gscore1, gscore2, gscore3, gscore4;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        gscore1 = GLOBAL_.player1Score;
        gscore2 = GLOBAL_.player2Score;
        gscore3 = GLOBAL_.player3Score;
        gscore4 = GLOBAL_.player4Score;
        UpdateText();
        _timer = 60.0f;
        timer.text = _timer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        UpdateTimer();
    }

    void UpdateText()
    {
        score1.text = gscore1.ToString();
        score2.text = gscore2.ToString();
        score3.text = gscore3.ToString();
        score4.text = gscore4.ToString();
    }

    void UpdateTimer()
    {
        if(_timer > 0)
            _timer -= Time.deltaTime;
        timer.text = _timer.ToString("f1");
    }
}
