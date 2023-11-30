using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Gun : MonoBehaviour
{
    [Tooltip("Prefab to shoot")]
    [SerializeField] private Bullet _bulletPrefab;
    [Tooltip("Bullet force")]
    [SerializeField] private float _muzzleVelocity = 700f;
    [Tooltip("End point of gun where shots appear")]
    [SerializeField] private Transform _muzzlePosition;
    [Tooltip("Fire rate")]
    [SerializeField] private float _currentRate = 0;
    private float _rate;
    string _rateKey = "Rate";
    [Tooltip("Year that gun belong = The rate multiply by 2 every 10 year")]
    [SerializeField] private int _currentYear = 10;
    private int _year;
    string _generationKey = "Generation";
    [Tooltip("Time in second that bullet last long, using as range value")]
    [SerializeField] private float _currentRange = 1;
    private float _range;
    string _rangeKey = "Range";

    private IObjectPool<Bullet> _objectPool;

    [SerializeField] private bool _collectionCheck = true;

    [SerializeField] private int _defaultCapacity = 20;
    [SerializeField] private int _maxSize = 100;
    [SerializeField] private GameController _gameController;
    [SerializeField] Player _player;
    [SerializeField] UIController _uiController;

    public bool _isTriggered = false;

    private float nextTimeToShoot;

    private void Awake()
    {
        _objectPool = new ObjectPool<Bullet>(CreateProjectile,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            _collectionCheck, _defaultCapacity, _maxSize);

    }
    private void Start()
    {
        _rate = PlayerPrefs.GetFloat(_rateKey);
        _range = PlayerPrefs.GetFloat(_rangeKey);
        _year = PlayerPrefs.GetInt(_generationKey);
        _currentRange += _range;
        _currentRate += _rate;
        _currentYear += _year;
        _uiController.UpdateYearValue(_currentYear);
        _uiController.UpdateRateValue(_currentRate);
        _uiController.UpdateRangeValue(_currentRange);
    }

    private Bullet CreateProjectile()
    {
        Bullet bulletInstance = Instantiate(_bulletPrefab);
        bulletInstance.ObjectPool = _objectPool;
        return bulletInstance;
    }

    private void OnReleaseToPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnGetFromPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(Bullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    private void FixedUpdate()
    {

        if (_gameController.IsPhase(GameController.GamePhase.Play) && Time.time > nextTimeToShoot && _objectPool != null)
        {
            Bullet bulletObject = _objectPool.Get();

            if (bulletObject == null)
                return;
            bulletObject.transform.SetPositionAndRotation(_muzzlePosition.position, _muzzlePosition.rotation);

            bulletObject.Multiplier = Mathf.FloorToInt(_currentYear / 10);

            bulletObject.GetComponent<Rigidbody>().AddForce(bulletObject.transform.up * _muzzleVelocity, ForceMode.Acceleration);

            bulletObject.Deactivate(1+_currentRange/5);

            nextTimeToShoot = Time.time + .1f + 30 / (_currentRate + 30);
        }
    }

    IEnumerator ResetPassCoroutine()
    {
        yield return new WaitForSeconds(1);
        _isTriggered = false;
    }
    public void ResetPass()
    {
        nextTimeToShoot = Time.time;
        StartCoroutine(ResetPassCoroutine());
    }
    public void SetRate(int delta)
    {
        _currentRate += delta;
        _uiController.UpdateRateValue(_currentRate);
    }
    public void SetRange(int delta)
    {
        _currentRange += delta;
        _uiController.UpdateRangeValue(_currentRange);
    }
    public void SetYear(int delta)
    {
        _currentYear += delta;
        _uiController.UpdateYearValue(_currentYear);
    }

    public void GameOver()
    {
        _gameController.ChangePhaseTo(GameController.GamePhase.Over);
        StartCoroutine(DyingCorourine());
    }
    IEnumerator DyingCorourine()
    {

        Rigidbody rb = GetComponent<Rigidbody>();
        GetComponent<CapsuleCollider>().isTrigger = false;
        rb.useGravity = true;
        rb.AddForce(UnityEngine.Random.Range(0, 200), UnityEngine.Random.Range(0, 200), UnityEngine.Random.Range(-200, 0), ForceMode.Acceleration);
        rb.AddTorque(UnityEngine.Random.Range(0, 20), UnityEngine.Random.Range(0, 20), UnityEngine.Random.Range(-20, 0), ForceMode.Acceleration);
        yield return null;
    }
    public void UpgradeRate()
    {
        if (_player.Money >= 1000)
        {
            _rate++;
            _currentRate++;
            _player.UpdateMoney(-1000);
            PlayerPrefs.SetFloat(_rateKey, _rate);
            _uiController.UpdateRateValue(_currentRate);
        }
    }
    public void UpgradeRange()
    {
        if (_player.Money >= 1000)
        {
            _range += .25f;
            _currentRange += .25f;
            _player.UpdateMoney(-1000);
            PlayerPrefs.SetFloat(_rangeKey, _range);
            _uiController.UpdateRangeValue(_currentRange);
        }
    }
    public void UpgradeGeneration()
    {
        if (_player.Money >= 1000)
        {
            _year += 2;
            _currentYear += 2;
            _player.UpdateMoney(-1000);
            PlayerPrefs.SetInt(_generationKey, _year);
            _uiController.UpdateYearValue(_currentYear);
        }
    }
}
