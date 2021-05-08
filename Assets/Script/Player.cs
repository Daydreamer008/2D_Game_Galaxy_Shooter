using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 5.0f;
    private float _speedMultiplier = 1.8f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1.0f;
    [SerializeField]
    private int _Lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    //private bool _isSpeedBoostActive = false;
    private bool _isSheildActive = false;
    [SerializeField]
    private GameObject _SheildVisualizer;
    [SerializeField]
    private int _score;
    private UIManager _uiManager; 

    void Start()
    {
        // take the current position = new position(0,0,0);
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Span Manager is NULL.");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMoment();
        // if i hit space key spawn game object
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMoment()
    {
        // Taking input for horizontal moments
        float HorizontalInput = Input.GetAxis("Horizontal");
        // new Vector3(1 , 0 , 0) * 5 * real time

        // if spped boost is false       
        // else speed boost multiplier

        transform.Translate(Vector3.right * HorizontalInput * _speed * Time.deltaTime);
 
        // taking inputs for vertical moments
        float VerticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * VerticalInput * _speed * Time.deltaTime);

        // we can take horizontal an d vertical transforms in one line only too 
        // transform.Translate(new Vector3(HorizontalInput , VerticalInput , 0) * _speed * Time.deltaTime);

        // if player position on vertical is greater than 0 then vertical = 0
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        // if player position on vertical is less than -1 then vertical = -4
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        // if player position on horizontal is greater than 11 then horizontal = -11
        if (transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        // if player position on horizontal is less than -11 then vertical = 11
        else if (transform.position.x <= -11.2f)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        // if sheilds are active do nothing
        if(_isSheildActive == true)
        {
            _isSheildActive = false;
            _SheildVisualizer.SetActive(false);
            return;
        }
        _Lives --;
        // communicate with spawn manager to stop spanning when lives  = 0

        _uiManager.UpdateLives(_Lives);

        // check if lives =0 , if yes destroy us
        if (_Lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        // triple shot active becomes true
        // start the power down coroutine for the power shot
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void speedBoostActive()
    {
        //_isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutiune());
    }

    IEnumerator SpeedBoostPowerDownRoutiune()
    {
        yield return new WaitForSeconds(5.0f);
        //_isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void sheildActive()
    {
        _isSheildActive = true;
        _SheildVisualizer.SetActive(true);
    }

    // method to add score
    // communicate with ui to upgrade the score 
    public void addScore()
    {
        _score += 10;
        _uiManager.updateScore(_score);
    }
} 
