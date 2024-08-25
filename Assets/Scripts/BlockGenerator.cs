using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject block;
    [SerializeField]
    GameObject block1,sideBlock,sideBlock2;
    [SerializeField]
    GameObject[] trees;
    public bool[,] obstacles;
    
    void Awake()
    {
        obstacles = new bool[20,10];
        for(int i=0;i<20;i++){
            for(int j=0;j<10;j++){
                GameObject tmpBlock;
                 obstacles[i,j] = false;
                if(j==0 || i==0 || j==9 || i==19){
                    if((i+j)%2 == 0){
                    tmpBlock = sideBlock;
                    }
                    else{
                    tmpBlock = sideBlock2;
                    }
                    Instantiate(tmpBlock,new Vector3(transform.position.x+i,transform.position.y,transform.position.z+j),Quaternion.identity);
                }
                else{
                    if((i+j)%2 == 0){
                    tmpBlock = block;
                    }
                    else{
                        tmpBlock = block1;
                        if(RandomCheck(16)){
                             obstacles[i,j] = true;
                            Instantiate(RandomGameObject(trees),new Vector3(transform.position.x+i,transform.position.y+0.43f,transform.position.z+j),Quaternion.identity);
                        }
                    }
                    Instantiate(tmpBlock,new Vector3(transform.position.x+i,transform.position.y,transform.position.z+j),Quaternion.identity);
                }
                }   
        }

        Pathfinding.grid = new Grid(20,10,obstacles);
    }
    bool RandomCheck(int _chance){
        int rnd = Random.Range(0,99);
        if(rnd<_chance){
            return true;
        }
        else{
            return false;
        }
    }
    GameObject RandomGameObject(GameObject[] _goList){
        int rnd = Random.Range(0,_goList.Length);
        return _goList[rnd];
        
    }
}
