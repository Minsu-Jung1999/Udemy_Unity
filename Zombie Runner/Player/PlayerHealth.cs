﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] float hitPoints = 100f;

    // 데미지에 따라 체력을 깍는 public 함수 만들기
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            // 작업: 죽었을 때 행동
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
