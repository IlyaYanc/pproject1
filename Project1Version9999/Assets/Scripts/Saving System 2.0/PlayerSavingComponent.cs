using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class PlayerSavingComponent : MonoBehaviour
{
    private PlayerSavingInfo plSavingInfo;
    [SerializeField] private List<HP> hps; 
    
    
    public void Save()
    {
        List<float> _hps = new List<float>();
        for (int i = 0; i < hps.Count; i++)
        {
            _hps.Add(100f);
        }
        
        
        for (var index = 0; index < hps.Count; index++)
        {
            if (hps[index].enabled)
                _hps[index] = hps[index].Hp();

        }
        

            
        plSavingInfo = new PlayerSavingInfo(GetComponent<Transform>().position, GetComponent<Transform>().rotation, _hps);
        SaveGame.Save("PlayerSaver", plSavingInfo);
    }

    public void Load()
    {
        if (SaveGame.Exists("PlayerSaver"))
        {
            plSavingInfo = SaveGame.Load<PlayerSavingInfo>("PlayerSaver");

            Transform tr = GetComponent<Transform>();
            tr.position = plSavingInfo.trPosition;
            tr.rotation = plSavingInfo.trRotation;

            for (var index = 0; index < hps.Count; index++)
            {
                if(hps[index].enabled)
                    hps[index].Hp(plSavingInfo.hps[index]);
            }
        }
    }
    
    class PlayerSavingInfo
    {
        public Vector3 trPosition;
        public Quaternion trRotation;
        public List<float> hps;

        public PlayerSavingInfo(Vector3 _trPosition, Quaternion _trRotation, List<float> _hps)
        {
            trPosition = _trPosition;
            trRotation = _trRotation;
            hps = _hps;
        }
    }
    
}


