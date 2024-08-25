using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Grid
{
   GameObject nodeVisual;
   float unit = 1.0f;
   int sizeX,sizeY;
   bool[,] obstacles;
   static int[] xOffsets = { -1, -1, -1, 0, 0, 1, 1, 1 };
   static int[] yOffsets = { -1, 0, 1, -1, 1, -1, 0, 1 };
   public Grid(int _sizeX,int _sizeY,bool[,] _obstacles){
    sizeX = _sizeX;
    sizeY = _sizeY;
    obstacles = _obstacles;
    //nodeVisual = _nodeVisual;
   }
    public Node[,] InitNodes(){
        Node[,] nodes =  new Node[sizeX,sizeY];
        for(int i = 0;i<sizeX;i++){
            for (int j = 0; j < sizeY; j++){
                //Vector3 _pos = new Vector3(unit*i,_parent.position.y,unit*j);
                //GameObject _go = GameObject.Instantiate(nodeVisual,_pos,Quaternion.identity,_parent);
                if(obstacles[i,j]){
                    nodes[i,j] = new Node(i,j,true);
                }
                else{
                    nodes[i,j] = new Node(i,j,false);
                }
            }
        }
        return nodes;
        
    }
    public static async Task<List<Node>> UpdateNeighbors(Node[,] _nodes,Node _curentNode,Node _targetNode)
    {
        return await UpdateNeighborCouroutine(_nodes,_curentNode,_targetNode);
    }
    static async Task<List<Node>> UpdateNeighborCouroutine(Node[,] _nodes,Node _curentNode,Node _targetNode){
        int _row =  _curentNode.x;
        int _col = _curentNode.y;
        Node[,] _grid = _nodes;
        List<Node> neighbors = new List<Node>();
        int sizeX = _grid.GetLength(0);
        int sizeY = _grid.GetLength(1);
        await Task.Delay(1);
        for (int i = 0; i < 8; i++)
        {
            int neighborsX = _row + xOffsets[i];
            int neighborsY = _col + yOffsets[i];
           
            if (neighborsX >= 0 && neighborsX < sizeX && neighborsY >= 0 && neighborsY < sizeY )
            {
                Node _neighbor = _grid[neighborsX,neighborsY];
                if(_neighbor.isObstacle || _neighbor.isClosed) continue;
                int _proposedGcost = 0;
                int _proposedHcost = Grid.GetManathanDistance(_neighbor,_targetNode);
                if(xOffsets[i] == 0 || yOffsets[i] == 0){
                    _proposedGcost= _curentNode.g + 10;
                }
                else{
                    _proposedGcost = _curentNode.g + 14;
                }
                if(_proposedGcost<_neighbor.g){
                    _neighbor.UpdateCost(_proposedGcost,_proposedHcost);
                    _neighbor.connectionNode = _curentNode;
                    //_neighbor.UpdateMaterial(Color.green);
                }
                else{
                    _neighbor.UpdateCost(_neighbor.g,_proposedHcost);
                }
                
                neighbors.Add(_neighbor);

            }
        }

        return neighbors;
    }
    public static int GetManathanDistance(Node _nodeA,Node _nodeB){
        int result = Mathf.Abs(_nodeA.x-_nodeB.x)*10 + Mathf.Abs(_nodeA.y-_nodeB.y)*10;
        return result; 
    }

}
