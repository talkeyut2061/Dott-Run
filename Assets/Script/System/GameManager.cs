using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //grographyは全て「まつ」に関する地名が入る
    [Header("Voice"), Tooltip("AudioClip")]
    [SerializeField] private AudioSource _voice;
    [SerializeField] private AudioClip[] _geoGraphyNameVoice;
    [SerializeField] private AudioClip _startVoice;
    [SerializeField] private AudioClip _mazuVoice;
    [SerializeField] private AudioClip _questionVoice;

    [Header("Button"), Tooltip("Button")]
    [SerializeField] private Button _start;
    [SerializeField] private Button _option;
    [SerializeField] private GameObject _selectButton;    //選択肢をまとめた
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;


    [Header("Text"), Tooltip("TMP_Text")]
    [SerializeField] private TMP_Text _titleText;      //タイトルUI
    [SerializeField] private TMP_Text _question;    //「質問」のテキスト
    [SerializeField] private TMP_Text _mainQuestion;    //出題する質問内容
    [SerializeField] private List<string> _questionText;    //質問内容
    [SerializeField] private List<string> _geoGraphyNameText;
    [SerializeField] private TMP_Text _endText;
    [SerializeField] private TMP_Text _pointText;           //得点テキスト

    [Header("Canvas"), Tooltip("CanvasCompornent")]
    [SerializeField] private CanvasGroup _titleCanvas;
    [SerializeField] private CanvasGroup _endCanvas;
    [SerializeField] private CanvasGroup _resultCanvas;

    [SerializeField] private Image _endBack;

    [SerializeField] private RectTransform _rect;

    [Header("Float")]
    [SerializeField] private float _maxHeight;
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
        _alpha = _titleCanvas.alpha;
    }

    // Update is called once per frame

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
            _titleCanvas.alpha = _alpha;
            yield return null;
        }

        title.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Select());
    }

    IEnumerator Select()
    {
        
        GameObject select = _selectButton;
        float speed = 1f;
        int random = Random.Range(0, _questionText.Count);
        string quest = _questionText[random];
        _mainQuestion.enabled = false;

        yield return new WaitForSeconds(speed);
        _question.enabled = false;
        select.SetActive(false);

        if (_questionText != null)
        {
            Debug.Log($"無事に通過する。 現在の評価点 : {_searchPoint}");
            _mainQuestion.text = quest;
            yield return new WaitForSeconds(speed);
            Debug.Log("Question");
            _voice.PlayOneShot(_questionVoice);
            _question.enabled = true;
            _yesButton.interactable = true;
            _noButton.interactable = true;
            yield return new WaitForSeconds(speed);
            _mainQuestion.enabled = true;
            _questionText.Remove(_questionText[random]);
            yield return new WaitForSeconds(speed);
            
            select.SetActive(true);
            
        }
    }
    

    public void SelectActive()
    {
        int p = Random.Range(1,20);
        _searchPoint += p;
        if (_questionText.Count >= 1)
        {
            StartCoroutine(Select());
        }
        else
        {
            StartCoroutine(FinishGame(_rect));
        }
    }
    IEnumerator FinishGame(RectTransform rectTrans)

    {   _endCanvas.alpha = _alpha;
        float speed = 1f;
        float EndSpeed = 150f;
        float CurrentHeight = rectTrans.rect.height;


        GameObject q = _question.gameObject;
        GameObject main = _mainQuestion.gameObject;
        GameObject end = _endText.gameObject;
        GameObject EndCanvas = _endCanvas.gameObject;
        GameObject EndBack = _endBack.gameObject;
        GameObject s = _selectButton;
        
        
        
        

        yield return new WaitForSeconds(speed);
        q.SetActive(false);
        s.SetActive(false);
        main.SetActive(false);
        EndBack.SetActive(true);

        yield return new WaitForSeconds(speed);
        Debug.Log($"ゲームを終了します。お疲れ様です！！！");
        while(!Mathf.Approximately(CurrentHeight, _maxHeight))
        {
            CurrentHeight = Mathf.MoveTowards(CurrentHeight, _maxHeight, EndSpeed * Time.unscaledDeltaTime);
            yield return null;
        }

        end.SetActive(true);
        EndCanvas.SetActive(true);
        yield return new WaitForSeconds(speed);
        while(!Mathf.Approximately(_alpha, _fadeIn))
        {
            _alpha = Mathf.MoveTowards(_alpha, _fadeIn, speed * Time.unscaledDeltaTime);
            _endCanvas.alpha = _alpha;
            yield return null;
        }

        end.SetActive(false);
        yield return new WaitForSeconds(speed);

        while(!Mathf.Approximately(_alpha,_fadeOut))
        {
            _alpha = Mathf.MoveTowards(_alpha, _fadeOut, speed * Time.unscaledDeltaTime);
            _endCanvas.alpha = _alpha;
            yield return null;
        }
        while (!Mathf.Approximately(_alpha,_fadeIn))
        {
            _alpha = Mathf.MoveTowards(_alpha,_fadeIn, speed * Time.unscaledDeltaTime);
        }
    }
}
