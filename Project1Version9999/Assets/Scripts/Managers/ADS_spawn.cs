using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private DeathAudioSourceController deathAudioSourceController;
    [SerializeField]
    private GameObject deathScreen;
    public bool deathOnBreakTrap;
    public Vector3 spawningPosition;
    // Start is called before the first frame update
    void Start()
    {
        deathOnBreakTrap = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        if(deathOnBreakTrap)
        {
            gameObject.transform.position = spawningPosition;
            deathOnBreakTrap = false;
        }
        Time.timeScale = 1;
        deathScreen.SetActive(false);
        deathAudioSourceController.EnableAudioSource();
        HP hp;
        hp = LeftDown.GetComponent<HP>();
        hp.GetHill(hp.GetMaxHp() * 0.75f);
        hp = RightDown.GetComponent<HP>();
        hp.GetHill(hp.GetMaxHp() * 0.75f);
        hp = LeftUp.GetComponent<HP>();
        hp.GetHill(hp.GetMaxHp() * 0.75f);
        hp = RightUp.GetComponent<HP>();
        hp.GetHill(hp.GetMaxHp() * 0.75f);
    }
}
