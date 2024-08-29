using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    protected static ScoreManager instance;
    public static ScoreManager Instance { get => instance; }

    [SerializeField] protected float _score;
    [SerializeField] protected float _highScore;
    [SerializeField] protected bool _is3Score = false;

    [SerializeField] protected Text scoreText;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PlayerPrefs.SetFloat("Score", 0);
    }

    private void Update()
    {
        this.Scoring();
        scoreText.text = "Score: " + Mathf.Round(_score);

    }

    private void LateUpdate()
    {
        if (this._score % 3 == 0 && this._score > 0 && !this._is3Score)
        {
            PlatformManager.Instance.GenerateRandomPlatform();
            this._is3Score = true;
        }
        if (this._score % 3 != 0)
        {
            this._is3Score = false;
        }
    }

    public virtual void Scoring()
    {
        int height = (int)PlayerController.Instance.gameObject.transform.position.y;
        if (PlayerPrefs.GetFloat("Score") <= height)
        {
            PlayerPrefs.SetFloat("Score", height);
            this._score = height;
        }
        else
        {
            this._score = PlayerPrefs.GetFloat("Score");
        }


    }

    public virtual void GeneratePlatformEach3Point()
    {



    }


}
