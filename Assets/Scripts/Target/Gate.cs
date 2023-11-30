using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gate : Target
{
    [SerializeField] TextMeshProUGUI _typeText;
    [SerializeField] TextMeshProUGUI _multiplierText;
    [SerializeField]PowerType _powerType;
    [SerializeField] RawImage _image;
    int _previousPower;
    int[] _startPowers= new int[5] {20,10,-20,-10,0};
    enum PowerType
    {
        Range,
        Rate,
        Year
    }

    private void Start()
    {
        _powerType = (PowerType)Random.Range(0, 3);
        _power = _startPowers[Random.Range(0,5)];
        _powerText.text = _power.ToString();
        _typeText.text = _powerType.ToString();
        _image.color = (_power<0)?Color.red:Color.green;
        _previousPower = _power;
    }
    private void FixedUpdate()
    {
        if (Mathf.Sign(_power)!=Mathf.Sign(_previousPower)) { StartCoroutine(ColorChangeCoutine()); }
    }
    private void OnTriggerEnter(Collider other)
    {
        OnGetShot(other);
        OnPass(other);
    }

    protected override void OnGetShot(Collider other)
    {
        if (other.TryGetComponent<Bullet>(out Bullet bullet) && !bullet._isTriggered)
        {
            bullet._isTriggered = true;
            bullet.Deactivate(0);
            _power += bullet.Multiplier;
            _powerText.text = _power.ToString();
        }
    }

    protected override void OnPass(Collider other)
    {
        if (other.TryGetComponent<Gun>(out Gun gun) && !gun._isTriggered)
        {
            gun._isTriggered = true;
            gun.ResetPass();
            switch (_powerType)
            {
                case PowerType.Range: gun.SetRange(_power); break;
                case PowerType.Rate: gun.SetRate(_power); break;
                case PowerType.Year: gun.SetYear(_power); break;

            }
            gameObject.SetActive(false);
        }
    }
    IEnumerator ColorChangeCoutine()
    {
        _image.color = Color.Lerp(_image.color, Color.green,.05f);
        yield return null;
    }
}
