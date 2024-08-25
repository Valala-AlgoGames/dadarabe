using System.Collections.Generic;
using UnityEngine;

public class PathFindingAgent : MonoBehaviour
{
    public List<Node> positions; // List of positions to move the Rigidbody through
    public float speed = 5f; // Movement speed
    public bool loop = false; // Whether to loop back to the start after reaching the end

    private Rigidbody rb;
    public int targetIndex = 0; // Index of the current target position
    public Vector3 instantTargetPosition; // Current target position
    public Vector3 targetPosition;
    IntCoord lastCoord = new IntCoord(-1,-1);
    GameObject zone;
    public bool isSearching = false;
    void Awake() {
        positions = new List<Node>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    void Update() {
       
    }
    public void GoTo(Vector3 _position){
        IntCoord pos = new IntCoord(new Vector2(_position.x,_position.z));
        if(pos.x == lastCoord.x && pos.y==lastCoord.y){
            return;
        }
        else{
            lastCoord = pos;
            new Pathfinding().FindPath(new IntCoord(new Vector2(transform.position.x,transform.position.z)),new IntCoord(new Vector2(_position.x,_position.z)),(res)=>{
            positions = res;
            targetIndex = positions.Count-1;
            if (positions.Count > 0)
            {
                instantTargetPosition = new Vector3(positions[targetIndex].GetPosition().x,0.45f,positions[targetIndex].GetPosition().y);
            }
             isSearching = true;
        });
        }
    }
    public void Stop(){
        isSearching = false;
    }
    void FixedUpdate()
    {
        if (positions.Count == 0 || !isSearching) return;
        Vector3 direction = (instantTargetPosition - rb.position).normalized;
        rb.velocity = (direction * speed);
        if (Vector3.Distance(rb.position, instantTargetPosition) < 0.15f )
        {
            if(targetIndex > 0){
                targetIndex--;
                instantTargetPosition = new Vector3(positions[targetIndex].GetPosition().x,transform.position.y,positions[targetIndex].GetPosition().y);
            }
            else{
                rb.velocity = Vector3.zero;
                isSearching = false;
            }

            
        }
    }

}
