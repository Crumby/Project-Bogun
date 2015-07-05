using UnityEngine;
using System.Collections;



public class WoodcutterBehaviour : MonoBehaviour {

    enum States{BORED, STILL, CUT, WALK, CARRY, FIGHT, WORK };

    private States behaviour = States.STILL;
    private Vector3 spawnPosition;
	private float timer = 0.0f;
    private GameObject Tree;
	Animator TreeAnim;
	public float moveSpeed = 2;

    public float ChopTime = 5.0f;
    public float FallTime = 0.95f;
    public float WorkTime = 10f;
    public float BoredTime = 5f;
    
    Animator anim;
	
	void Start () {
        anim = GetComponent<Animator>();
        spawnPosition = transform.position;

	}
	
	void Update () {
        
        switch (behaviour)
        {
            case States.STILL:
                //timer += Time.deltaTime;
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Static"))
                {
                    anim.Play("Static");       
                }
                Tree = findTree();
				TreeAnim = Tree.GetComponent<Animator>();
                if (Tree != null)
                {
					behaviour = States.WALK;
                    timer = 0;
                }
                /*
                if (timer > BoredTime)
                {
                    behaviour = States.WALK;
                    timer = 0;
                }*/
                break;
            case States.WALK:
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
					transform.rotation = Quaternion.LookRotation(transform.position - Tree.transform.position, Vector3.up);
                    anim.Play("Walk"); 
                }
				if (Tree == null)
				{
					behaviour = States.STILL;
				}
				transform.position = Vector3.MoveTowards(transform.position, Tree.transform.position, moveSpeed*Time.deltaTime);
				if (transform.position == Tree.transform.position)
				{
					behaviour = States.CUT;
				}
                break;
            case States.CUT:
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
					TreeAnim.Play("Cutting");
                    anim.Play("Attack"); 
                }
				if (Tree == null)
				{
					behaviour = States.CARRY;
				}
                timer += Time.deltaTime;
                if (timer > ChopTime)
                {
					if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Static"))
					{
						TreeAnim.Play("FallDown");
						anim.Play("Static");       
					}

                    if (timer > ChopTime + FallTime)
                    {
                        timer = 0;
						Destroy(Tree, 0.0f);
                        Tree = null;
                        behaviour = States.CARRY;
                    }
                }
                break;
            case States.CARRY:
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                { 
					transform.rotation =  Quaternion.LookRotation(transform.position - spawnPosition, Vector3.up);
                    anim.Play("Walk");
                }
				
				transform.position = Vector3.MoveTowards(transform.position, spawnPosition, moveSpeed*Time.deltaTime);
                if (transform.position == spawnPosition)
                {
                    behaviour = States.STILL;
                }
                break;
            case States.WORK:
                //make planks
                break;
            case States.BORED:
                break;
            case States.FIGHT:
                //defend
                break;
        }

	
	}

	private GameObject findTree() {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Tree");
		float distance = Mathf.Infinity;
		GameObject closest = null;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	public void OnTriggerEnter(Collider other)
	{	
			behaviour = States.CUT;
	}

}
;