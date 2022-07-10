using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ADS_spawn : MonoBehaviour
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
    private GameObject allPlayer;
    [SerializeField]
    private float HP_DecreaseValue;
    [SerializeField]
    private bool progressiveHPDecrease;
    [SerializeField]
    private DeathAudioSourceController deathAudioSourceController;
    [SerializeField]
    private GameObject deathScreen;
    public bool deathOnBreakTrap;
    public Vector3 spawningPosition;
    public breacTrap trap_killer;
    private int death_amount = 0;
    // Start is called before the first frame update
    void Start()
    {
        deathOnBreakTrap = false;
    }

    // Update is called once per frame

    [Button]
    public void Spawn()
    {
        RightDown.GetComponentInParent<DamageInputController>().Undeath();
        StartCoroutine(enemiTupit(2.5f));
        Debug.Log(deathOnBreakTrap);
        if(deathOnBreakTrap)
        {
            allPlayer.transform.position = spawningPosition;
            deathOnBreakTrap = false;
            trap_killer.Activate();
        }
        Time.timeScale = 1;
        deathScreen.GetComponent<Start_Death_Screen>().DisActivate();
        deathAudioSourceController.EnableAudioSource();
        HP hp;
        hp = LeftDown.GetComponent<HP>();
        hp.Hp(hp.GetMaxHp() * HPMultipluyer());
        hp = RightDown.GetComponent<HP>();
        hp.Hp(hp.GetMaxHp() * HPMultipluyer());
        hp = LeftUp.GetComponent<HP>();
        hp.Hp(hp.GetMaxHp() * HPMultipluyer());
        hp = RightUp.GetComponent<HP>();
        hp.Hp(hp.GetMaxHp() * HPMultipluyer());

        death_amount++;
    }
    IEnumerator enemiTupit(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<enemy>().CanAttack(true);
        }
    }

    private float HPMultipluyer()
    {
        float decrease = HP_DecreaseValue;
        if(progressiveHPDecrease == true)
        {
            decrease *= death_amount;
        }
        float mult = 1f - decrease;
        if(mult - HP_DecreaseValue <= 0f)
        {
            deathScreen.GetComponent<Start_Death_Screen>().DisableAdButton();
        }
        return mult;
    }
}
