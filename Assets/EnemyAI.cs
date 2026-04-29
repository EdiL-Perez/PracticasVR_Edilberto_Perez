using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform Player;
    public float rango = 10f;
    private NavMeshAgent agente;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    
    {
        if(Player != null){
            float distancia = Vector3.Distance(transform.position,Player.position);
            if(distancia <= rango){
                agente.SetDestination(Player.position);
                
            }
            else if (agente.hasPath)
            {
                agente.ResetPath();
            }
        }
        
    }
}
