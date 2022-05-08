using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]
    private float maxhp;
    [SerializeField]
    private float meleeResist;
    [SerializeField]
    private float prickResist;
    [SerializeField]
    private float rangeResist;
    [SerializeField]
    private float magicResist;
    [SerializeField]
    private RectTransform hpCanvas;
    [SerializeField]
    private Slider hpSlid;
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private Image ArmorBreakImage;
    [SerializeField]
    private Image fireImage;
    [SerializeField]
    private GameObject background;

    private float brokenArmorTimer = 0;
    class Dot
    {
        float time;
        float damage;
        public Dot(float Damage, float Time)
        {
            time = Time;
            damage = Damage;
        }
        public Dot()
        {
            time = 0;
            damage = 0;
        }
        public float Damage()
        {
            return damage;
        }
        public float Time()
        {
            return time;
        }
        public void Time(float minusTime)
        {
            time -= minusTime;
        }
    }
    private List<Dot> dots = new List<Dot>();
    private float hp;
    // Start is called before the first frame update

    void Start()
    {
        hp = maxhp;

    }

    public void SetMaxHP()
    {
        hp = maxhp;
    }
    public float ReturnHP()
    {
        return hp;
    }
    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject,0.005f);
            Destroy(hpCanvas.gameObject);
        }
        hpCanvas.position = transform.position + new Vector3(0, 0.7f, 0);
        hpSlid.value = hp / maxhp;
        if(brokenArmorTimer>0)
        {
            ArmorBreakImage.GetComponent<Image>().enabled = true;
        }
        else
        {
            ArmorBreakImage.GetComponent<Image>().enabled = false;
        }
        if(dots.Count>0)
        {
            fireImage.GetComponent<Image>().enabled = true;
        }
        else
        {
            fireImage.GetComponent<Image>().enabled = false;
        }
        if ((hp / maxhp)>0.66)
        {
            fillImage.color = Color.green;
        }
        else if((hp / maxhp) > 0.33)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }
        for (int i = 0; i < dots.Count; i++)
        {
            if (dots[i].Time() > 0)
            {
                dots[i].Time(Time.deltaTime);
                hp -= dots[i].Damage() * (1 - magicResist)*Time.deltaTime;
            }
            else
            {
                dots.RemoveAt(i);
            }
        }
        if (brokenArmorTimer > 0)
        {
            brokenArmorTimer -= Time.deltaTime;
            if (brokenArmorTimer < 0)
            {
                brokenArmorTimer = 0;
            }
        }
        else
        {
            brokenArmorTimer = 0;
        }
        if (gameObject.GetComponent<enemy>().hasDisquiet)
        {
            fillImage.gameObject.SetActive(true);
            background.SetActive(true);
            ArmorBreakImage.gameObject.SetActive(true);
            fireImage.gameObject.SetActive(true);
        }
        if (!gameObject.GetComponent<enemy>().hasDisquiet)
        {
            fillImage.gameObject.SetActive(false);
            background.SetActive(false);
            ArmorBreakImage.gameObject.SetActive(false);
            fireImage.gameObject.SetActive(false);
        }
    }

    public void GetDamage(float damage, weaponType WeaponType, float fireTime, float armorBreakTime)
    {
        if (WeaponType == weaponType.Knight)
        {
            hp -= damage * (1 - meleeResist);
            brokenArmorTimer += armorBreakTime;
        }
        else if (WeaponType == weaponType.Thief)
        {
            if (dots.Count > 0)
                hp -= damage * (1 - prickResist) * 1.8f;
            else
                hp -= damage * (1 - prickResist);
        }
        else if (WeaponType == weaponType.Archer)
        {
            if (brokenArmorTimer > 0)
                hp -= damage * (1 - rangeResist) * 1.5f;
            else
                hp -= damage * (1 - rangeResist);
        }
        else if (WeaponType == weaponType.Mage)
            dots.Add(new Dot(damage, fireTime));
    }
}
