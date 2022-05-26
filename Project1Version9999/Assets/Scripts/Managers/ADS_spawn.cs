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
    private DeathAudioSourceController deathAudioSourceController;
    [SerializeField]
    private GameObject deathScreen;
    public bool deathOnBreakTrap;
    public Vector3 spawningPosition;
    public breacTrap trap_killer;
    // Start is called before the first frame update
    void Start()
    {
        deathOnBreakTrap = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Button]
    public void Spawn()
    {
        RightDown.GetComponentInParent<DamageInputController>().Undeath();
        StartCoroutine(enemiTupit(1.5f));
        if(deathOnBreakTrap)
        {
            allPlayer.transform.position = spawningPosition;
            trap_killer.Activate();
            deathOnBreakTrap = false;
        }
        Time.timeScale = 1;
        deathScreen.GetComponent<Start_Death_Screen>().DisActivate();
        deathAudioSourceController.EnableAudioSource();
        HP hp;
        hp = LeftDown.GetComponent<HP>();
        hp.Hp(hp.GetMaxHp() * 0.75f);
        hp = RightDown.GetComponent<HP>();
        hp.Hp(hp.GetMaxHp() * 0.75f);
        hp = LeftUp.GetComponent<HP>();
        hp.Hp(hp.GetMaxHp() * 0.75f);
        hp = RightUp.GetComponent<HP>();
        hp.Hp(hp.GetMaxHp() * 0.75f);
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
}
