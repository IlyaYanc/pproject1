using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInputController : MonoBehaviour
{
    [SerializeField]
    private HP LeftUp;
    [SerializeField]
    private HP RightUp;
    [SerializeField]
    private HP LeftDown;
    [SerializeField]
    private HP RightDown;
    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private ADS_spawn ADS_Spawning;

    [SerializeField]
    private DeathAudioSourceController deathAudioSourceController;

    private bool DEATH = false;
    public enum DamageType { melee, range, rangeSplash, meleeSplash, magic, fire /*used only in enemy script*/, rangeFire /*used only in enemy script*/}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(LeftUp.Hp()<=0 && LeftDown.Hp() <= 0 && RightDown.Hp()<=0 && RightUp.Hp()<=0 && !DEATH)
        {
            Death();
            DEATH = true;
        }
    }
    public void Undeath()
    {
        DEATH = false;
    }
    public void TakeDamage(Vector3 damageSoursePosition, float damage, DamageType damageType)
    {
        Vector3 dir = damageSoursePosition - transform.position;
        float angel = (Mathf.Atan2(dir.y, dir.x) - Mathf.Atan2(transform.up.y, transform.up.x)) * Mathf.Rad2Deg;
        if (angel > 180)
        {
            angel -= 360;
        }
        else if(angel<(-180))
        {
            angel += 360;
        }
        HP[] hpses = new HP[4];
        if (angel <= 45 && angel >= (-45))
        {
            hpses[0] = LeftUp;
            hpses[1] = RightUp;
            hpses[2] = LeftDown;
            hpses[3] = RightDown;
        }
        else if (angel > 45 && angel <= 135)
        {
            hpses[0] = LeftUp;
            hpses[1] = LeftDown;
            hpses[2] = RightUp;
            hpses[3] = RightDown;
        }
        else if (angel < (-45) && angel >= (-135))
        {

            hpses[0] = RightUp;
            hpses[1] = RightDown;
            hpses[2] = LeftUp;
            hpses[3] = LeftDown;
        }
        else
        {
            hpses[0] = LeftDown;
            hpses[1] = RightDown;
            hpses[2] = LeftUp;
            hpses[3] = RightUp;
        }
        switch (damageType)
        {
            case DamageType.melee:
                hpses[0].GetDamege(damage);
                hpses[1].GetDamege(damage);
                hpses[2].GetDamege(damage * 0.25f);
                hpses[3].GetDamege(damage * 0.25f);
                break;
            case DamageType.range:
                hpses[0].GetDamege(damage);
                hpses[1].GetDamege(damage);
                hpses[2].GetDamege(damage * 0.35f);
                hpses[3].GetDamege(damage * 0.35f);
                break;
            case DamageType.rangeSplash:
                hpses[0].GetDamege(damage);
                hpses[1].GetDamege(damage);
                hpses[2].GetDamege(damage*0.75f);
                hpses[3].GetDamege(damage*0.75f);
                break;
            case DamageType.meleeSplash:
                hpses[0].GetDamege(damage);
                hpses[1].GetDamege(damage);
                hpses[2].GetDamege(damage);
                hpses[3].GetDamege(damage);
                break;
            case DamageType.magic:
                hpses[0].GetDamege(damage * 0.5f);
                hpses[1].GetDamege(damage * 0.5f);
                hpses[2].GetDamege(damage);
                hpses[3].GetDamege(damage);
                break;
        }
    }
    public void TakeDamage(float fireDamage, float fireTime)
    {
            LeftDown.GetDamege(fireDamage, fireTime);
            LeftUp.GetDamege(fireDamage, fireTime);
            RightDown.GetDamege(fireDamage, fireTime);
            RightUp.GetDamege(fireDamage, fireTime);
    }
    private void Death()
    {
        deathScreen.GetComponent<Start_Death_Screen>().Activate();
        if(deathAudioSourceController != null)
        {
            deathAudioSourceController.DisableAudioSources();
        }
          
    }
    public void DeathOnBreakTrap(Vector3 SpawningPosition)
    {
        ADS_Spawning.spawningPosition = SpawningPosition;
        ADS_Spawning.deathOnBreakTrap = true;
        deathScreen.GetComponent<Start_Death_Screen>().Activate();
        if (deathAudioSourceController != null)
        {
            deathAudioSourceController.DisableAudioSources();
        }
    }
    public void killAll()
    {
        LeftDown.GetDamege(LeftDown.Hp());
        LeftUp.GetDamege(LeftUp.Hp());
        RightDown.GetDamege(RightDown.Hp());
        RightUp.GetDamege(RightUp.Hp());
    }
}
