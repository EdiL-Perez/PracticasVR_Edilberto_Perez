using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform Player;
    public float rango = 10f;
    private NavMeshAgent agente;
    private Animator anim;

    public AudioSource reproductorMusica; 
    public AudioClip musicaExploracion; 
    public AudioClip musicaEnemigo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    
    {
        if(Player != null){
            float distancia = Vector3.Distance(transform.position,Player.position);
            if(distancia <= rango){
                CambiarPista(musicaEnemigo);
                anim.SetBool("Running",true);
                agente.SetDestination(Player.position);
                
            }
            else if (agente.hasPath)
            {
                CambiarPista(musicaExploracion);
                anim.SetBool("Running",false);
                agente.ResetPath();
            }
        }
        
    }

    void CambiarPista(AudioClip nuevaPista)
    {
        if (reproductorMusica.clip == nuevaPista) return;
        
        reproductorMusica.Stop(); 
        reproductorMusica.clip = nuevaPista; 
        reproductorMusica.Play(); 
    }
}
