using UnityEngine;

public class Donut : MonoBehaviour
{
    [Header("�������� ��������.")]
    [SerializeField] private float _speed = 5;
    [Header("������ �����.")]
    [SerializeField] private DrawClick _drawClick = null;
    [Header("����� �� FMOD")]
    [SerializeField] private string _eventClick = null;
    [Header("UI ������ ��� ���������� ������� ����.")]
    [SerializeField] private Camera _camera = null;

    private Transform _transform = null;

    //��� ������ � �������.
    private Data _data = Data.GetInstance();
    private DataTemp _json = null;

    private void Awake()
    {
        _json = _data.Output();
        _transform = this.transform;

        if (_camera == null)
            _camera = Camera.main;
    }

    //���� �� ������ ������.
    private void OnMouseUp()
    {
        if (!_data.IsOpenMenu)
        {
            _json.Balance += _json.Click;

            if (_eventClick != null)
                if (_eventClick != "")
                    FMODUnity.RuntimeManager.PlayOneShot(_eventClick);

            if (_drawClick != null)
            {
                DrawClick drawClick = Instantiate(_drawClick, _transform.parent);

                if (_camera == null)
                    drawClick.transform.position = new Vector3(_transform.position.x, _transform.position.y, _drawClick.transform.position.z);
                else
                    drawClick.transform.position = new Vector3(_camera.ScreenToWorldPoint(Input.mousePosition).x / 25f, _camera.ScreenToWorldPoint(Input.mousePosition).y / 25f, .2f);
            }

        }
    }

    //�������� �������.
    void FixedUpdate() => _transform.Rotate(0, _speed * Time.deltaTime, 0, Space.Self);
}
