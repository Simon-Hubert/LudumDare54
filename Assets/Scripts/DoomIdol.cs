using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomIdol : MonoBehaviour
{
    [SerializeField] Sprite _phase2;
    [SerializeField] Sprite _phase3;

    SpriteRenderer _sr;
    int _phase=1;

    void Start(){
        _sr = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(){
        _phase++;
        switch(_phase){
            case 2:
                _sr.sprite = _phase2;
                break;
            case 3:
                _sr.sprite = _phase3;
                break;
        }
    }
}
