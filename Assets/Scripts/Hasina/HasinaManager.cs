using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasinaManager : MonoBehaviour
{
    public static HasinaManager instance;
    [SerializeField]BlockGenerator blockGenerator;
    [SerializeField]int MaxHasina = 6;
    [SerializeField] GameObject moara;
    public List<GameObject> moaraList;
    // Update is called once per frame
    void Awake() {
        if(instance!=null){
            Destroy(instance);
        }
        instance = this;

        moaraList = new List<GameObject>();
    }
    void Start()
    {
        StartCoroutine("GenerateMoaraInit");
    }
    public void GetMoara(GameObject _moara){
        GetComponent<AudioSource>().Play();
        moaraList.Remove(_moara);
        Destroy(_moara);
        StartCoroutine("GenerateMoaraReplace")
;    }
    IEnumerator GenerateMoaraInit(){
        GenerateRandomMoara();
        yield return new WaitForSeconds(Random.Range(3,7));
        if(moaraList.Count<6){
            StartCoroutine("GenerateMoaraInit");
        }
    }
    IEnumerator GenerateMoaraReplace(){
        yield return new WaitForSeconds(Random.Range(3,7));
        if(moaraList.Count<6){
        GenerateRandomMoara();
        }
    }
    void GenerateRandomMoara(){
        bool continueGenerate = true;
        int x = 0,y = 0;
        while(continueGenerate){
             x = Random.Range(1,19);
             y = Random.Range(1,9);
            if(blockGenerator.obstacles[x,y]){
                continueGenerate = true;
            }
            else{
                continueGenerate = false;
            }
        }
        
        moaraList.Add(GameObject.Instantiate(moara,new Vector3(x,0.5f,y),Quaternion.identity));
    }
}
