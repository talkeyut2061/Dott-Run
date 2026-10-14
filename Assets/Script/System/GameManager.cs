using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //grographyは全て「まつ」に関する地名が入る
    [Header("Voice"), Tooltip("AudioClip")]
    [SerializeField] private AudioClip[] _geoGraphyNameVoice;
    [SerializeField] private AudioClip _startVoice;
    [SerializeField] private AudioClip _mazuVoice;

    [Header("Button"), Tooltip("Button")]
    [SerializeField] private Button _start;
    [SerializeField] private Button _option;
    [SerializeField] private GameObject _selectButton;


    [Header("Text"), Tooltip("TMP_Text")]

    [SerializeField] private TMP_Text _titleText;      //タイトルUI
    [SerializeField] private TMP_Text _question;    //「質問」のテキスト
    [SerializeField] private TMP_Text _mainQuestion;    //出題する質問内容
    [SerializeField] private List<string> _questionText;    //質問内容
    [SerializeField] private TMP_Text _pointText;           //得点テキスト

    [Header("Canvas"), Tooltip("CanvasCompornent")]
    [SerializeField] private CanvasGroup _canvasGroup;

    private float _alpha;
    private float _fadeIn = 1f;
    private float _fadeOut = 0f;

    private int _searchPoint;


    void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _alpha = _canvasGroup.alpha;
        //ButtonActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ButtonActive()
    {
        if (_start.interactable == false)
        {
            StartCoroutine(GameStart());
        }

        else if (_option.interactable == false)
        {
            Debug.Log($"現在、開発中");
        }
    }






    private IEnumerator GameStart()
    {

        GameObject title = _titleText.gameObject;
        float speed = 0.5f;

        while (!Mathf.Approximately(_alpha, _fadeOut))
        {
            _alpha = Mathf.MoveTowards(_alpha, _fadeOut, speed * Time.unscaledDeltaTime);
            _canvasGroup.alpha = _alpha;
            yield return null;
        }

        title.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Select());
    }

    IEnumerator Select()
    {
        GameObject select = _selectButton;

        int random = UnityEngine.Random.Range(1, _questionText.Count);
        string quest = _questionText[random];

  
        if (_questionText != null)
        {
            Debug.Log("無事に通過する");
            _mainQuestion.text = quest;
            yield return new WaitForSeconds(1f);
            _question.enabled = false;
            yield return new WaitForSeconds(1f);
            Debug.Log("Question");
            _question.enabled = true;
            yield return new WaitForSeconds(3f);
            _mainQuestion.enabled = true;
            _questionText.Remove(_questionText[random]);
            yield return new WaitForSeconds(1f);
            select.SetActive(true);
            
        }
        else
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(FinishGame());
        }

    }

    IEnumerator FinishGame()
    {
        yield return null;
    }
}
