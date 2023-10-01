using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private Vector2 _direction;
    private UnityEvent _onHit;
    private float _lifeTime = 1.0f;
    private Rigidbody2D _rb;

    public Vector2 Direction { get => _direction; set => _direction = value; }
    public float Speed { get => _speed; set => _speed = value; }

    private void Awake() {
        _rb = transform.GetComponent<Rigidbody2D>();
    }
    
    private void Start() {
        Destroy(gameObject, _lifeTime);
        _rb.velocity = Direction * Speed;
    }

    private void Update() {
        float projectileAngle = Vector3.SignedAngle(Vector3.right, _rb.velocity, Vector3.forward);
        transform.rotation = Quaternion.Euler(0,0,projectileAngle);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            _onHit.Invoke();
            Destroy(gameObject);
        }
    }
}
