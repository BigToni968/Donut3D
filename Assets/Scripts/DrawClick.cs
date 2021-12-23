using TMPro;
using UnityEngine;

public class DrawClick : MonoBehaviour
{
    [Header("Скорость движения текста.")]
    [SerializeField] private float _speed = 1;
    [Header("Время жизни текста.")]
    [SerializeField] private float _timeLive = 5;

    private Data _data = Data.GetInstance();
    private DataTemp _json = null;
    private TextMeshPro _text = null;
    private float _second = 0;
    private Formatting _formatting = null;

    private void Awake()
    {
        _json = _data.Output();
        _text = GetComponent<TextMeshPro>();
        _formatting = new Formatting();
    }
    private void Start() => _text.text = "+" + _formatting.ToText(_json.Click);

    private void OnEnable() => transform.position =
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y), 0);


    private void FixedUpdate()
    {
        _second += Time.deltaTime;

        if (_second > _timeLive)
            Destroy(gameObject);

        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
}
