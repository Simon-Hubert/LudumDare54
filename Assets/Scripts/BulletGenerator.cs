using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] private Pattern _testPattern;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private int _patternCursor;
    [SerializeField] private List<Pattern> _playListPatterns;

    public int PatternCursor { 
        get => _patternCursor;
        set{
            _patternCursor = value % _playListPatterns.Count;
        }
    }

    private void Start() {
        PatternCursor = 0;
    }

    public void PlayList(){
        if(_playListPatterns.Count <= 0){
            throw new ArgumentException("Playlist vide !!!");
        }
        else{
            PlayPattern(_playListPatterns[PatternCursor]);
            PatternCursor ++;
        }
    }


    public void PlayPattern(Pattern pattern){
        StartCoroutine(PlayPatternCoroutine());

        IEnumerator PlayPatternCoroutine(){
            float angleStep = pattern.ProjectileAngle / (pattern.NumberProjectile-1);
            float angle = pattern.ProjectileAngleOffset;

            for(int i = 0; i < pattern.NumberProjectile; i++){
                Vector2 direction = new Vector2(Mathf.Sin(angle*Mathf.PI/180),Mathf.Cos(angle*Mathf.PI/180));
                Projectile projSave = Instantiate(_projectile, transform).GetComponent<Projectile>();
                projSave.Direction = direction;
                projSave.Speed = pattern.ProjectileSpeed;
                if(pattern.ProjectileDelay > 0){
                    yield return new WaitForSeconds(pattern.ProjectileDelay);
                }
                angle += angleStep;
            }
            foreach (Pattern compositePattern in pattern.CompositePattern)
            {
                PlayPattern(compositePattern);
            }
            
        }
    }

    [Button] public void TestPattern(){
        PlayPattern(_testPattern);
    }
}
