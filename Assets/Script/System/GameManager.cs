using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Button _selectOneButton;
    [SerializeField] private Button _selectThreeButton;


    [Header("Text"), Tooltip("TMP_Text")]

    [SerializeField] private TMP_Text _titleText;      //タイトル
    [SerializeField] private TMP_Text _questionTemplate;    //「質問」のテキスト
    [SerializeField] private List<string> _questionText;    //質問内容
    [SerializeField] private List<Button> _select;          //選択肢
    

    [Header("Canvas"), Tooltip("CanvasCompornent")]
    [SerializeField] private CanvasGroup _canvasGroup;

    private float _alpha;
    private float _fadeIn = 1f;
    private float _fadeOut = 0f;


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

        else if(_option.interactable == false)
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
        yield return new WaitForSeconds(1f);
        int random = UnityEngine.Random.Range(1, _questionText.Count);
        string Question = _questionText[random];
        



        yield return new WaitForSeconds(1f);
    }
}
