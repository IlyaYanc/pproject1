using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class HP : MonoBehaviour
{
    [SerializeField]
    private Button Attack_B;
    [SerializeField]
    private Slider hpSlider;
    class PereodicDamager
    {
        private float _time;
        private float _damage;
        public PereodicDamager(float damage, float time)
        {
            _time = time;
            _damage = damage;
        }
        public PereodicDamager()
        {
            _time = 0;
            _damage = 0;
        }
        public float Damage()
        {
            return _damage;
        }
        public float Time()
        {
            return _time;
        }
        public void Time(float minusTime)
        {
            _time -= minusTime;
        }
    }
    private List<PereodicDamager> dots = new List<PereodicDamager>();
    [SerializeField]
    private float HPmax = 100;
    private float resist = 1;
    private float hp;

    private void Start()
    {
        hp = HPmax;
        if (hpSlider != null)
            hpSlider.maxValue = 1;
        dots.Clear();
    }
    public float Hp()
    {
        return hp;
    }
    public float GetMaxHp()
    {
        return HPmax;
    }
    public float Hp(float newHp)
    {
        hp = newHp;
        return hp - newHp;
    }
    public void GetDamege(float fireDamage, float fireTime)
    {
        dots.Add(new PereodicDamager(fireDamage * resist, fireTime));
    }
    public void GetDamege(float damage)
    {
        hp -= damage * resist;
    }
    public void GetHill(float hill)
    {
        hp = hp + hill;
        if (hp > HPmax)
            hp = HPmax;
    }
    [Button]
    private void Death()
    {
        Attack_B.interactable = false;
    }
    private void Update()
    {
        //смэрть
        if (hp <= 0)
        {
            Death();
        }
        //UI
            hpSlider.value = hp / HPmax;
        //доты
        for (int i = 0; i < dots.Count; i++)
        {
            if (dots[i].Time() > 0)
            {
                dots[i].Time(Time.deltaTime);
                hp -= dots[i].Damage() * Time.deltaTime;
            }
            else
            {
                dots.RemoveAt(i);
            }
        }
    }

    public void SetResist(float _resist)
    {
        resist = _resist;
    }
    public float GetResist()
    {
        return resist;
    }
    public void DecreaseResist(float _x)
    {
        resist -= _x;
    }
    public void EncreaseResist(float _x)
    {
        resist += _x;
    }
}
