using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformManager : MonoBehaviour
{
    [SerializeField] List<FallingPlatform> m_PlatformList;

    public void ChoosePlatform(int number = 1)
    {
        while(number > 0)
        {
            int rand = Random.Range(0, m_PlatformList.Count);
            if (!m_PlatformList[rand].IsFalling)
            {
                m_PlatformList[rand].StartFalling();
                number--;
            }
        }
    }

    public void ResetAll()
    {
        foreach (var platform in m_PlatformList) { platform.ResetPlatform(); }
    }

    [Button]
    void DEBUG_ChoosePlat() => ChoosePlatform();
    [Button]
    void DEBUG_ResetALL() => ResetAll();
}
