using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{

    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour{

    private Rigidbody rb;
    private AudioSource audioSource;

    public float speed; // it's public so we can set and change it directly in unity - editable property
    public Boundary bound;
    public float tilt;

    public GameObject shot;
    public GameObject shotSpawn;

    public float fireRate;
    private float nextFire = 0.0f;

    private Quaternion calibrationQuaternion;
    private SimpleTouchPad simpleTouchPad;
    private SimpleTouchAreaButton areaButton;

    // Start is called before the first frame update
    void Start(){
        
        GameObject gameControllerObject1 = GameObject.FindWithTag("SimpleTouchPad");
        if (gameControllerObject1 != null)
        {
            simpleTouchPad = gameControllerObject1.GetComponent<SimpleTouchPad>();
        }
        GameObject gameControllerObject2 = GameObject.FindWithTag("SimpleTouchAreaButton");
        if (gameControllerObject2 != null)
        {
            areaButton = gameControllerObject2.GetComponent<SimpleTouchAreaButton>();
        }

        CalibrateAccelerometer();

        this.rb = GetComponent<Rigidbody>();
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButton("Fire1") && Time.time > nextFire){
        if (areaButton.CanFire() && Time.time > nextFire) { 
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation); //as GameObject;
            audioSource.Play();
        }
    }

    //will be called automatically by unity just before each fixed physics step
    void FixedUpdate(){

        //for standalone WebGl
        //float moveHorizontal = Input.GetAxis("Horizontal"); // GetAxis always return  number 0-1, so by default moving slowly
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //for Simple touch pad
        //Vector2 direction = simpleTouchPad.GetDirection();
        //Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);

        Vector3 accelerationRaw = Input.acceleration; // by  Vector3(0.0f, 0.0f, -1.0f)
        Vector3 acceleration = FixAcceleration(accelerationRaw); // by initial zero point

        Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y);

        this.rb.velocity = movement * speed;

        //x - left/right z - forward/backward
        this.rb.position = new Vector3(

                Mathf.Clamp(this.rb.position.x, bound.xMin, bound.xMax),
                0.0f,
                Mathf.Clamp(this.rb.position.z, bound.zMin, bound.zMax)
        );
        //we nedd tilt along z axis
        this.rb.rotation = Quaternion.Euler(0.0f, 0.0f, this.rb.velocity.x * -tilt);
    }

    //Used to calibrate the Iput.acceleration input
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }
} 
