using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 0.3f;
    [SerializeField] private float _maxDistance;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private List<CubeConfigStorage> _cubeConfigs;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _maxCubeValue;
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private MobAdsRewarder _ads;

    private GameObject _cube;
    private bool _cubeIsControlling = true;
    private const string SCORE = "Score: ";
    private float _scoreForUpdate = 100;

    public int ScoreCount { get; set; } = 0;

    private void Awake()
    {
        _cube = Instantiate(_cubePrefab, _spawnPoint);
        _cube.GetComponent<MovingCube>().SetParametrs(gameObject.GetComponent<GameManager>(), _cubeConfigs[Random.Range(0, _maxCubeValue)]);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && _cubeIsControlling)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f); 
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); 
            if( objPosition.x < _maxDistance && objPosition.x > _maxDistance * -1)
                _cube.transform.position = new Vector3(objPosition.x, _spawnPoint.position.y, _spawnPoint.position.z);
            _cube.GetComponent<Rigidbody>().freezeRotation = true;
        }
        if(Input.GetMouseButtonUp(0) && _cube != null)
        {
            ScoreCount += _cube.GetComponent<MovingCube>().Value;
            _scoreText.text = SCORE + ScoreCount.ToString();
            StartCoroutine(CreateNewCube());
        }

        if (ScoreCount > _scoreForUpdate)
        {
            _maxCubeValue++;
            _scoreForUpdate *= 2.5f;
        }
    }

    private IEnumerator CreateNewCube()
    {
        _cube.GetComponent<Rigidbody>().freezeRotation = false;
        _cube.GetComponent<MovingCube>().StartMove();
        _cubeIsControlling = false;
        _cube = null;
        _ads.ShootCount++;
        yield return new WaitForSeconds(_spawnDelay);
        _cube = Instantiate(_cubePrefab, _spawnPoint);
        _cube.GetComponent<MovingCube>().SetParametrs(gameObject.GetComponent<GameManager>(), _cubeConfigs[Random.Range(0, _maxCubeValue)]);
        _cubeIsControlling = true;
    }

    public void CreateNewCube(MovingCube cube)
    {
        for (int i = 0; i < _cubeConfigs.Count; i++)
        {
            if (System.Convert.ToInt32(_cubeConfigs[i].CubeValue) == cube.Value)
            {
                try
                {
                    cube.SetParametrs(gameObject.GetComponent<GameManager>(), _cubeConfigs[i + 1]);
                    cube.gameObject.GetComponent<MovingCube>().Jump();
                }
                catch
                {
                    throw new System.Exception("game was end");
                }
                break;
            }
        }
    }
}
