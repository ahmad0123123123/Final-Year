

using UnityEngine;
using UnityEngine.SceneManagement;
namespace DeadSilence
{
    public class FPSController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 1f;
        public float crouchSpeed = 0.4f;
        public float runSpeedMultiplier = 2f;
        public float jumpForce = 10f;
        public float level=0;
        public float crouchHeight = 0.5f;
        private bool crouch = false;

        [Header("MouseLook Settings")]

        private Vector2 clampInDegrees = new Vector2(360, 180);
        public bool lockCursor;
        public Vector2 sensitivity = new Vector2(0.5f, 0.5f);
        public Vector2 smoothing = new Vector2(3, 3);
        
        [HideInInspector]
        public Vector2 targetDirection;

        [HideInInspector]
        public Rigidbody controllerRigidbody;
        
        private CapsuleCollider controllerCollider;
        public Transform camHolder;
        private float moveSpeedLocal;

        Vector2 _mouseAbsolute;
        Vector2 _smoothMouse;
        
        private float distanceToGround;

        private Animator weaponHolderAnimator;

        public bool isClimbing = false;

        private float inAirTime;

        [HideInInspector]
        public bool mouseLookEnabled = true;

        //Velocity calculation variable
        private Vector3 previousPos = new Vector3();
        
        Vector3 dirVector;

        
        public GameObject objective1;
        public GameObject objective2;
        public bool findsomeone = false;
        public GameObject findsomeone1;
        public GameObject Game_Returntoofficer;
        public GameObject turnonelectricity;
        public GameObject computerlog;
        public GameObject lightson;
        public GameObject disperse;
        public bool getVaccinebool = false;
        public bool findmedkit = false;
        public bool Returntoofficer = false;
        public bool objectivecomplete = false;
        InputManager inputManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Gas Can")
            {
                
                print("colliding");
                objectivecomplete = true;
                Destroy(other.gameObject);
                objective1.SetActive(false);
                objective2.SetActive(true);
            }
            if (other.gameObject.tag == "Car" && objectivecomplete==true)
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (other.gameObject.tag == "Reach Police Station")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (other.gameObject.tag == "Find Someone")
            {
                Destroy(other);
                findsomeone1.SetActive(true);
                Time.timeScale = 0;

            }
            if (other.gameObject.tag == "Find Medkit")
            {
                findmedkit = true;
            }
            if (other.gameObject.tag == "Return to Officer" && findmedkit==true)
            {
                Returntoofficer = true;
                //Time.timeScale=0;
                Game_Returntoofficer.SetActive(true);
            }
            if (other.gameObject.tag == "Exit the building" && Returntoofficer==true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (other.gameObject.tag == "Sewers")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (other.gameObject.tag == "finallevel")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (other.gameObject.tag == "Electricity")
            {
                Destroy(other);
                turnonelectricity.SetActive(true);
            }
            if (other.gameObject.tag == "computerlog")
            {
                Destroy(other);
                computerlog.SetActive(true);
            }
            if (other.gameObject.tag == "Electricityturnedon")
            {
                Destroy(other);
                GameObject.Find("Point lightc2").GetComponent<Light>().enabled=true;
                GameObject.Find("Point lightc1").GetComponent<Light>().enabled = true;
                lightson.SetActive(true);
            }
            if (other.gameObject.tag == "getVaccine")
            {
                Destroy(other);
                getVaccinebool = true;
                Destroy(GameObject.Find("Bottle4"));
                disperse.SetActive(true);
            }
            if(other.gameObject.tag =="disperse" && getVaccinebool==true)
            {
                SceneManager.LoadScene(0);
            }
            if(other.gameObject.tag == "sewer1111")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }
        private void Start()
        {
            
            controllerRigidbody = GetComponent<Rigidbody>();
            controllerCollider = GetComponent<CapsuleCollider>();

            distanceToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
            targetDirection = camHolder.transform.forward;
            weaponHolderAnimator = GameObject.Find("Weapon holder").GetComponent<Animator>();

            inputManager = FindObjectOfType<InputManager>();
        }

        private void Update()
        {
            
            //Debug.Log(SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
            
            //objective1.GetComponent(tag);
            //Debug.Log(isGrounded());
            if (mouseLookEnabled && !InventoryManager.showInventory)
                MouseLook();

            if (!InputManager.useInput)
            {
                StandaloneMovement();
            }
            else
            {
                
            }

            if (lockCursor && !InputManager.useInput)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            Crouch();
            Landing();
        }

        void StandaloneMovement()
        {
            if (isGrounded())
            {
                if (CheckMovement())
                {
                    weaponHolderAnimator.SetBool("Walk", true);
                    moveSpeedLocal = moveSpeed;
                }
                else
                    weaponHolderAnimator.SetBool("Walk", false);

                if (Input.GetKey(inputManager.Run) && !isClimbing && !crouch && weaponHolderAnimator.GetBool("Walk") == true)
                {
                    moveSpeedLocal = runSpeedMultiplier * moveSpeed;
                    weaponHolderAnimator.SetBool("Run", true);
                }
                else
                    weaponHolderAnimator.SetBool("Run", false);
            }
            else
            {
                weaponHolderAnimator.SetBool("Walk", false);
                weaponHolderAnimator.SetBool("Run", false);
            }

            if (crouch)
            {
                moveSpeedLocal = crouchSpeed;
                weaponHolderAnimator.SetBool("Walk", false);
                weaponHolderAnimator.SetBool("Run", false);
                if (CheckMovement())
                {
                    weaponHolderAnimator.SetBool("Crouch", true);
                }
                else
                    weaponHolderAnimator.SetBool("Crouch", false);
            }
            else
                weaponHolderAnimator.SetBool("Crouch", false);

            if (Input.GetKeyDown(inputManager.Crouch))
            {
                crouch = !crouch;
            }
            if (Input.GetKeyDown(inputManager.Jump))
            {
                Jump();
                crouch = false;
            }
        }

        
        
        void FixedUpdate()
        {
            CharacterMovement();
        }
        
        void CharacterMovement()
        {
            
            var camForward = camHolder.transform.forward;
            var camRight = camHolder.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();
            
            if (isClimbing)
            {
                crouch = false;

                weaponHolderAnimator.SetBool("HideWeapon", true);
                controllerRigidbody.useGravity = false;

                
                if (!InputManager.useInput)
                {
                    dirVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
                }
                
                
                Vector3 verticalDirection = transform.up;
                Vector3 moveDirection = (verticalDirection) * dirVector.z+ camRight * dirVector.x;
                
                controllerRigidbody.MovePosition(transform.position + moveDirection * moveSpeedLocal * Time.deltaTime);
            }
            else
            {
                weaponHolderAnimator.SetBool("HideWeapon", false);
                controllerRigidbody.useGravity = true;

                if (!InputManager.useInput)
                {
                    dirVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
                }
                else if(InputManager.useInput)
                {
                    dirVector = new Vector3(InputManager.joystickInputVector.x, 0, InputManager.joystickInputVector.y);
                }
                Vector3 moveDirection = camForward * dirVector.z + camRight * dirVector.x;

                controllerRigidbody.MovePosition(transform.position + moveDirection * moveSpeedLocal * Time.deltaTime);
            }
        }

        bool CheckMovement()
        {
            if (!InputManager.useInput)
            {
                if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
                {
                    return true;
                }
            }

            

            return false;
        }
        
        void MouseLook()
        {
                Quaternion targetOrientation = Quaternion.Euler(targetDirection);

                Vector2 mouseDelta = new Vector2();

                
                if (!InputManager.useInput)
                {
                    mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
                }

                mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

                _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
                _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

                _mouseAbsolute += _smoothMouse;

                if (clampInDegrees.x < 360)
                    _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

                var xRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right);
            camHolder.transform.localRotation = xRotation;

                if (clampInDegrees.y < 360)
                    _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

                var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, camHolder.transform.InverseTransformDirection(Vector3.up));
            camHolder.transform.localRotation *= yRotation;
            camHolder.transform.rotation *= targetOrientation;
        }
        
        void Crouch()
        {
            if (crouch == true)
            {
                controllerCollider.height = crouchHeight;
                camHolder.transform.localPosition = new Vector3(0, 0.2f, 0);
            }
            else
            {
                Ray ray = new Ray();
                RaycastHit hit;
                ray.origin = transform.position;
                ray.direction = transform.up;
                if (!Physics.Raycast(ray, out hit, 1))
                {
                    camHolder.transform.localPosition = new Vector3(0, 0.4f, 0);
                    controllerCollider.height = 1.6f;
                    crouch = false;
                }
                else
                    crouch = true;
            }
        }
        
        public float GetVelocityMagnitude()
        {
            var velocity = ((transform.position - previousPos).magnitude) / Time.deltaTime;
            previousPos = transform.position;
            return velocity;
        }

        public void Jump()
        {
            if (isGrounded())
                controllerRigidbody.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }

        public void CrouchMobile()
        {
            crouch = !crouch;
        }

        bool isGrounded()
        {
            //distancetoGround is condition.
            return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
        }

        void Landing()
        {
            if(!isGrounded())
            {
                
                inAirTime += Time.deltaTime;
            }
            else
            {
                if (inAirTime > 0.5f)
                    weaponHolderAnimator.Play("Landing");

                inAirTime = 0;
            }
        }
    }
}
