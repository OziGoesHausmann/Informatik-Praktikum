using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		public GameObject pos;
		public GameObject cam;
		public bool facingRight;
		public bool arrived;
		IAstarAI ai;

		void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;

		}

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;


		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update () {

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			

			if (Input.GetMouseButtonDown(0) && hit.collider.tag == "Station" || Input.GetMouseButtonDown(0) && hit.collider.tag == "Trash")
			{
				pos = hit.collider.gameObject;
				target = pos.transform;
			}



			if (target != null && ai != null)
			{
				ai.destination = target.position;
			}

			if (ai.destination == target.transform.position)
            {
				this.enabled = false;
				target = null;
            }
		}
	}
}
