using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Control _control;
    private Camera _camera;
    private LineRenderer _lineRenderer;

    private bool _canShoot = true;

    public Transform _directionToShoot;
    public GameObject _projectile;

    [SerializeField]
    [Range(0.5f, 2f)]private float _minDelayShoot;

    [SerializeField]
    [Range(2.1f, 5f)]private float _maxDelayShoot;


    private void Awake()
    {
        _control = new Control();
        _camera = Camera.main;
        
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        _control.Enable();
    }

    private void OnDisable()
    {
        _control.Disable();
    }

    private void Start()
    {
        _control.Gameplay.Shoot.performed += ctx => Shoot();
    }

    private void Update()
    {
        Vector2 mouseScreenPosition = _control.Gameplay.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 targetDirection = mouseWorldPosition - transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * 90;

        if ( angle > 0 && angle < 190)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            _lineRenderer.SetPosition(0, new Vector3(_directionToShoot.position.x, _directionToShoot.position.y, _directionToShoot.position.z));
            _lineRenderer.SetPosition(1, transform.position);
        }
            
    }

    private void Shoot()
    {
        if (!_canShoot) { return ;}
        _canShoot = false;
        Instantiate(_projectile, _directionToShoot.position, _directionToShoot.rotation);

        StartCoroutine(DelayShoot());
        //se tiver carregado pode atirar
    }

    private IEnumerator DelayShoot()
    {
        float delayShoot = UnityEngine.Random.Range(_minDelayShoot, _maxDelayShoot);

        yield return new WaitForSeconds(delayShoot);
        _canShoot = true;
    }
}
