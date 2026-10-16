using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private TMP_Text _titleText;      //タイトルのテキスト
    [SerializeField] private TMP_Text _sitsumonText;    //「まずは質問」のテキスト
    [SerializeField] private TMP_Text _question;        //出題する質問内容
    [SerializeField] private List<string> _questionText;    //出題する質問内容のリスト
    [SerializeField] private List<string> _geoGraphyNameText; //地名のテキスト
    [SerializeField] private TMP_Text _endText;             //「終了」のテキスト
    [SerializeField] private TMP_Text _resultText;         //「診断結果」のテキスト
    [SerializeField] private TMP_Text _scoreText;           //得点テキスト
    [SerializeField] private TMP_Text _onePhraseText;       //診断結果の一言

    [Header("Canvas"), Tooltip("CanvasCompornent")]
    [SerializeField] private CanvasGroup _titleCanvas;    
    [SerializeField] private CanvasGroup _endCanvas;      //ゲーム終了時のImageコンポーネント   
    [SerializeField] private Image _endBack;              //「終了」テキストの親
    [SerializeField] private RectTransform _rect;

    [Header("Float")]
    [SerializeField] private float _maxHeight;
    private float _alpha;                            //α値
    private float _fadeIn = 1f;                      
    private float _fadeOut = 0f;

    private int _searchPoint;                        //得点


    void Awake()
    {
        _alpha = _titleCanvas.alpha;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

     private void ButtonActive()
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
        float speed = 1.5f;
        int random = Random.Range(0, _questionText.Count);
        string quest = _questionText[random];
       

        yield return new WaitForSeconds(speed); 
        _question.enabled = false;
        _sitsumonText.enabled = false;
        select.SetActive(false);

        Debug.Log($"無事に通過する。 現在の評価点 : {_searchPoint}");
        _question.text = quest;
        yield return new WaitForSeconds(speed);

        Debug.Log("Question");
        _voice.PlayOneShot(_questionVoice);
        _sitsumonText.enabled = true;
        _yesButton.interactable = true;
        _noButton.interactable = true;
        yield return new WaitForSeconds(speed);

        _question.enabled = true;
        _questionText.Remove(_questionText[random]);
        yield return new WaitForSeconds(speed);

        select.SetActive(true);
    }


    private void SelectActive()
    {
        int p = Random.Range(1, 20);
        _searchPoint += p;
        if (_questionText.Count >= 1)
        {
            StartCoroutine(Select());
        }
        else
        {
            StartCoroutine(FinishGame());
        }

    }
    IEnumerator FinishGame()
    {
            _endCanvas.alpha = _alpha;
            float speed = 1f;
            GameObject q = _sitsumonText.gameObject;
            GameObject m = _question.gameObject;
            GameObject EndCanvas = _endCanvas.gameObject;
            GameObject EndBack = _endBack.gameObject;
            GameObject Select = _selectButton;
            GameObject Score = _scoreText.gameObject;

            yield return new WaitForSeconds(speed);
            q.SetActive(false);
            Select.SetActive(false);
            m.SetActive(false);

            yield return new WaitForSeconds(speed);
            Debug.Log($"ゲームを終了します。お疲れ様です！！！");
            EndBack.SetActive(true);
            EndCanvas.SetActive(true);
            yield return new WaitForSeconds(speed);
            while (!Mathf.Approximately(_alpha, _fadeIn))
            {
                _alpha = Mathf.MoveTowards(_alpha, _fadeIn, speed * Time.unscaledDeltaTime);
                _endCanvas.alpha = _alpha;
                yield return null;
            }
            
            //end.SetActive(false);
            yield return new WaitForSeconds(speed);
            EndBack.SetActive(false);
        
        
        while (!Mathf.Approximately(_alpha, _fadeOut))
            {
                _alpha = Mathf.MoveTowards(_alpha, _fadeOut, speed * Time.unscaledDeltaTime);
                _endCanvas.alpha = _alpha;
                yield return null;
            } 
        EndCanvas.SetActive(false);
        _resultText.enabled = true;
        yield return new WaitForSeconds(speed);

        _scoreText.text = $"得点  {_searchPoint}" + "点";
        yield return new WaitForSeconds(speed);

        int n = Random.Range(1, _geoGraphyNameText.Count);
        yield return new WaitForSeconds(speed);

        _scoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(speed);
        _onePhraseText.gameObject.SetActive(true);
        _onePhraseText.text += $"{_geoGraphyNameText[n]}";
    }
}
