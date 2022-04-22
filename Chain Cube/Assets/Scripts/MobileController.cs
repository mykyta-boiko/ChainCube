
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _spawnDelay = 0.3f;
    [SerializeField] private float _maxDistance;
    [SerializeField] private List<GameObject> _cubePrefab;
    [SerializeField] private GameObject _swipeDetectorObject;
    [SerializeField] private Transform _spawnPoint;

    private GameObject _cube;
    private bool _cubeIsControlling = true;

    private void Awake()
    {
        _cube = _cube = Instantiate(_cubePrefab[Random.Range(0, _cubePrefab.Count)], _spawnPoint);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && _cubeIsControlling)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f); // переменной записываються координаты мыши по иксу и игрику
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
            if( objPosition.x < _maxDistance && objPosition.x > _maxDistance * -1)
                _cube.transform.position = new Vector3(objPosition.x, _spawnPoint.position.y, _spawnPoint.position.z); // и собственно объекту записываються координаты
        }
        if(Input.GetMouseButtonUp(0) && _cube != null)
        {
            _cube.GetComponent<MovingCube>().StartMove();
            StartCoroutine(CreateNewCube());
        }
    }

    private IEnumerator CreateNewCube()
    {
        _cubeIsControlling = false;
        _cube = null;
        yield return new WaitForSeconds(_spawnDelay);
        _cube = Instantiate(_cubePrefab[Random.Range(0, _cubePrefab.Count)], _spawnPoint);
        _cubeIsControlling = true;
    }


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {

    }
    public virtual void OnDrag(PointerEventData eventData)
    {
       // _cubePrefab.transform.position = new Vector3(Input.mousePosition.x, _moveVector.position.y, _moveVector.position.z);
    }

}
