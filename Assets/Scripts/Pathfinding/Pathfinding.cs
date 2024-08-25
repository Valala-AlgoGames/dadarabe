using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class Pathfinding
{
    public static Grid grid;
    [SerializeField]GameObject node;

    List<Node> closedList = new List<Node>();
    List<Node> researchList = new List<Node>();
    Node curentNode = null;
    public Pathfinding(){
      
    }
    public void FindPath(IntCoord _startPosition,IntCoord _targetPosition,System.Action<List<Node>> _callback){
        Node[,] nodes = Pathfinding.grid.InitNodes();
        Node start = nodes[_startPosition.x,_startPosition.y];
        start.g = 0;
        ResearchPath(nodes,start,start,nodes[_targetPosition.x,_targetPosition.y]).ContinueWith((task)=>{
            _callback?.Invoke(task.Result);
        });
       /* foreach (Node item in path)
        {
            Debug.Log(item.x+" "+item.y);
        }*/
    }
    public async Task<List<Node>> ResearchPath(Node[,] _nodes,Node _current,Node _start,Node _target){
        curentNode = _current;
        closedList.Add(curentNode); 
       // curentNode.UpdateMaterial(Color.blue);           
        curentNode.isClosed = true;
        if(_current==_target){
            return GetPath(_target,_start,new List<Node>());
        }
        researchList.AddRange(await Grid.UpdateNeighbors(_nodes,curentNode,_target));
        if(researchList.Count == 0){
            return null;
        }
        Node _bestNode = null;
        foreach (Node _node in researchList)
        {
            if(_bestNode == null){_bestNode = _node;}
            else{
                if(_bestNode.f>_node.f){
                    _bestNode = _node;
                }
                else if(_bestNode.f==_node.f){
                    if(_bestNode.h>_node.h){
                        _bestNode = _node;
                    }
                }
            }
        }
        researchList.Remove(_bestNode);
        return await ResearchPath(_nodes,_bestNode,_start,_target);
    }
    private List<Node> GetPath(Node _current,Node _start,List<Node> _list){
        _list.Add(_current);
        if(_current == _start){
            return _list;
        }
        return GetPath(_current.connectionNode,_start,_list);
    }
}
