using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [Header("Пончик")]
    [SerializeField] private GameObject _donut = null;
    [Header("Скорость вращения пончика")]
    [SerializeField] private float _speed = 5;

    [Header("Активировать фэйковую загрузку?")]
    [SerializeField] private bool _isFakeLoad = false;
    [Header("Время загрузки")]
    [Range(1, 10)]
    [SerializeField] private int _fakeTime = 5;

    private Scrollbar _bar = null;
    private float _second = 0;

    private void Awake() => _bar ??= FindObjectOfType<Scrollbar>();

    void Start()
    {
        if (_donut != null)
        {
            if (!_isFakeLoad)
                StartCoroutine(LoadAsync());
            else
                StartCoroutine(LoadFake());
        }
        else
            Debug.Log("Donut не вложен!");
    }

    private IEnumerator LoadAsync()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (_bar != null)
            {
                _bar.size = async.progress < .9f ? async.progress : 1f;
                if (_bar.size == 1f)
                    break;
            }
            yield return null;
        }
        async.allowSceneActivation = true;
    }

    private IEnumerator LoadFake()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        async.allowSceneActivation = false;
        while (_second < _fakeTime)
        {
            _second += _fakeTime / _fakeTime;
            _bar.size += 1f / _fakeTime;
            yield return new WaitForSeconds(1);
        }
        async.allowSceneActivation = true;
    }

    private void FixedUpdate()
    {
        if (_donut != null)
            _donut.transform.Rotate(0, _speed * Time.deltaTime, 0, Space.World);
    }
}
