using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Inspector prefab reference")]
    public GameObject customerPrefab;

    [Header("Agent reference")]
    private Chef chefAgent;
    private Waiter waiterAgent;
    private List<Customer> customerAgentList = new List<Customer>();


    void Start()
    {
        chefAgent = GameObject.Find("Chef").GetComponent<Chef>();
        chefAgent.Initialize();
        waiterAgent = GameObject.Find("Waiter").GetComponent<Waiter>();
        waiterAgent.Initialize();

        for (int i = 0; i < 3; i++)
        {
            Customer temp = Instantiate(customerPrefab, new Vector2(3.5f, 7 + i * 2), Quaternion.identity).GetComponent<Customer>();
            temp.Initialize();
            customerAgentList.Add(temp);
        }
    }

    
    void Update()
    {
        float time = Time.deltaTime;
        chefAgent.Tick(time);
        waiterAgent.Tick(time);

        foreach (Customer c in customerAgentList)
        {
            c.Tick(time);
        }
    }
}
