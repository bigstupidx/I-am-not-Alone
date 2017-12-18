using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.





        private Vector2 m_Input;
        Rigidbody rigidbody;
        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private bool m_cursorIsLocked = true;
        float theForwardDirection;
        private void Start ()
        {
            rigidbody = GetComponent<Rigidbody>();
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;

            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update ()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate ()
        {
            var directionVectorForward = m_Cam.transform.forward;
            var directionVectorRight = m_Cam.transform.right;
            //No downward movement
            directionVectorForward.y = 0;
            directionVectorRight.y = 0;
            //No downward movement
            directionVectorForward.Normalize();
            directionVectorRight.Normalize();
            //Scale it by how fast the user wants to move

            directionVectorRight *= CrossPlatformInputManager.GetAxis("Horizontal");
            directionVectorForward *= CrossPlatformInputManager.GetAxis("Vertical");

            float yRot = CrossPlatformInputManager.GetAxis("Mouse X");
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y");
            Vector3 targetDirection = new Vector3(yRot, 0f, xRot);
            targetDirection = m_Cam.transform.TransformDirection(targetDirection);
            targetDirection.y = 0.0f;
            bool crouch = Input.GetKey(KeyCode.C);
            //  m_Input = new Vector2(h, v);

            // normalize input if it exceeds 1 in combined length:

            // calculate move direction to pass to character
            // calculate camera relative direction to move:
            //    m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            //   m_Move = v * transform.forward + h * transform.right;


            var angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
            m_CharacterTargetRot = Quaternion.Euler(0, angle, 0);



            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }
            m_Move = directionVectorForward + directionVectorRight;



            if (yRot != 0f || xRot != 0f)
            {

                //  transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg, transform.eulerAngles.z);


                transform.localRotation = m_CharacterTargetRot;

                // transform.localRotation = Quaternion.Slerp(transform.localRotation, m_CharacterTargetRot,
                //5 * Time.deltaTime);

            }
            else
            {

                MovementManagement(m_Move.x, m_Move.z);
            }





#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
        void MovementManagement (float horizontal, float vertical)
        {
            // Set the sneaking parameter to the sneak input.


            // If there is some axis input...
            if (horizontal != 0f || vertical != 0f)
            {
                // ... set the players rotation and set the speed parameter to 5.5f.
                Rotating(horizontal, vertical);

            }

            // Otherwise set the speed parameter to 0.

        }


        void Rotating (float horizontal, float vertical)
        {
            // Create a new vector of the horizontal and vertical inputs.
            Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);

            // Create a rotation based on this new vector assuming that up is the global y axis.
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            // Create a rotation that is an increment closer to the target rotation from the player's rotation.
            Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, 5f * Time.deltaTime);

            // Change the players rotation to this new rotation.
            GetComponent<Rigidbody>().MoveRotation(newRotation);
        }

    }

}

