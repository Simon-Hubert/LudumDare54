using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    //TW : Je le fait pas en UI

    [SerializeField] AnimationCurve _curve;
    SpriteRenderer _sr;
    [SerializeField]
    private int _turnNumber;
    [SerializeField]
    float _duration;

    private void Start() {
        _sr = GetComponent<SpriteRenderer>();
    }
    
    public void SplashCall(){
        _sr.enabled = true;
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation(){
        float maxAngle = 360*_turnNumber;
        float timer = 0f;
        while (timer < _duration){
            float w = _curve.Evaluate(timer/_duration);
            float angle = maxAngle*w;
            transform.localScale = new Vector3(w,w,w);
            transform.eulerAngles = new Vector3 (0,0,angle);
            timer += Time.deltaTime;
            yield return 0;
        }
        transform.eulerAngles = new Vector3 (0,0,maxAngle);
        _sr.enabled = false;
    }
}
