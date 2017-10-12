using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour {

	public LayerMask tileMask;
    public LayerMask coinMask;
    public LayerMask enemyMask;
    public LayerMask endMask;
    public LayerMask deathMask;
    public LayerMask switcherMask;

	const float skinWidth = .015f;
	public int horizontalRayCount = 5;
	public int verticalRayCount = 5;

	float horizontalRaySpacing;
	float verticalRaySpacing;

    BoxCollider2D collider_check;
    RaycastOrigins raycastOrigins;
	public CollisionInfo collisions;

    void Start() {
		collider_check = GetComponent<BoxCollider2D> ();
		CalculateRaySpacing ();

	}

	public void Move(Vector3 velocity) {
		UpdateRaycastOrigins ();
		collisions.Reset ();

		if (velocity.x != 0) {
			HorizontalCollisions (ref velocity);
        }
		if (velocity.y != 0) {
			VerticalCollisions (ref velocity);
        }

		transform.Translate (velocity);
	}

	void HorizontalCollisions(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;
		
		for (int i = 0; i < horizontalRayCount; i ++) {
			Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit_tile =     Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, tileMask);
            RaycastHit2D hit_coin =     Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, coinMask);
            RaycastHit2D hit_enemy =    Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, enemyMask);
            RaycastHit2D hit_end =      Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, endMask);
            RaycastHit2D hit_switcher = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, switcherMask);
            RaycastHit2D hit_death =    Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, deathMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength,Color.red);

            //check tile hit
			if (hit_tile) {

                //get closest tile to feet
                GameObject feet = GameObject.Find("Player_Feet");
                GameObject tilMin = null;
                float tilDis = Mathf.Infinity;
                Vector3 currentLocation = feet.transform.position;
                foreach (GameObject tile in GameObject.FindGameObjectsWithTag("tile")) {
                    float dis = Vector3.Distance(tile.transform.position, currentLocation);
                    if (dis < tilDis) {
                        tilMin = tile;
                        tilDis = dis;
                    }
                }
                gameObject.transform.SetParent(tilMin.transform);

                velocity.x = (hit_tile.distance - skinWidth) * directionX;
				rayLength = hit_tile.distance;
				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}
            if (!hit_tile) {
                gameObject.transform.SetParent(GameObject.Find("WORLD").transform);
            }

            //check coin hit
            if (hit_coin) {
                //find closest item
                //NOTE: not best method, but its the easiest one as of now
                GameObject coinMin = null;
                float coinDis = Mathf.Infinity;
                Vector3 currentLocation = this.gameObject.transform.position;
                foreach (GameObject coin in GameObject.FindGameObjectsWithTag("coin")) {
                    float dis = Vector3.Distance(coin.transform.position, currentLocation);
                    if (dis < coinDis) {
                        coinMin = coin;
                        coinDis = dis;
                    }
                }
                Destroy(coinMin);
                collisions.coin = true;
            }
            //check enemy hit
            if (hit_enemy) {
                collisions.enemy = true; ;
            }
            //check end hit
            if (hit_end) {
                collisions.end = true;
            }
            //check switcher hit
            if (hit_switcher) {
                //find closest item
                //NOTE: not best method, but its the easiest one as of now
                GameObject switcherMin = null;
                float switcherDis = Mathf.Infinity;
                Vector3 currentLocation = this.gameObject.transform.position;
                foreach (GameObject swatch in GameObject.FindGameObjectsWithTag("switcher")) {
                    float dis = Vector3.Distance(swatch.transform.position, currentLocation);
                    swatch.GetComponent<SwitchController>().hasFocus = false;
                    if (dis < switcherDis) {
                        switcherMin = swatch;
                        switcherDis = dis;
                    }
                }
                switcherMin.GetComponent<SwitchController>().hasFocus = true;
                collisions.switcher = true;
            }
            //check death hit
            if (hit_death) {
                collisions.death = true;
            }
        }
	}

	void VerticalCollisions(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i ++) {
			Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit_tile =     Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, tileMask);
            RaycastHit2D hit_coin =     Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, coinMask);
            RaycastHit2D hit_enemy =    Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, enemyMask);
            RaycastHit2D hit_end =      Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, endMask);
            RaycastHit2D hit_switcher = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, switcherMask);
            RaycastHit2D hit_death =    Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, deathMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionY * rayLength, Color.red);

            //check tile hit
            if (hit_tile) {

                //get closest tile to feet
                GameObject feet = GameObject.Find("Player_Feet");
                GameObject tilMin = null;
                float tilDis = Mathf.Infinity;
                Vector3 currentLocation = feet.transform.position;
                foreach (GameObject tile in GameObject.FindGameObjectsWithTag("tile")) {
                    float dis = Vector3.Distance(tile.transform.position, currentLocation);
                    if (dis < tilDis) {
                        tilMin = tile;
                        tilDis = dis;
                    }
                }
                gameObject.transform.SetParent(tilMin.transform);

                velocity.y = (hit_tile.distance - skinWidth) * directionY;
                rayLength = hit_tile.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
            if (!hit_tile) {
                gameObject.transform.SetParent(GameObject.Find("WORLD").transform);
            }

            //check coin hit
            if (hit_coin) {
                //find closest item
                //NOTE: not best method, but its the easiest one as of now
                GameObject coinMin = null;
                float coinDis = Mathf.Infinity;
                Vector3 currentLocation = this.gameObject.transform.position;
                foreach (GameObject coin in GameObject.FindGameObjectsWithTag("coin")) {
                    float dis = Vector3.Distance(coin.transform.position, currentLocation);
                    if (dis < coinDis) {
                        coinMin = coin;
                        coinDis = dis;
                    }
                }
                Destroy(coinMin);
                collisions.coin = true;
            }
            //check enemy hit
            if (hit_enemy) {
                collisions.enemy = true; ;
            }
            //check end hit
            if (hit_end) {
                collisions.end = true;
            }
            //check switcher hit
            if (hit_switcher) {
                //find closest item
                //NOTE: not best method, but its the easiest one as of now
                GameObject switcherMin = null;
                float switcherDis = Mathf.Infinity;
                Vector3 currentLocation = this.gameObject.transform.position;
                foreach (GameObject swatch in GameObject.FindGameObjectsWithTag("switcher")) {
                    float dis = Vector3.Distance(swatch.transform.position, currentLocation);
                    swatch.GetComponent<SwitchController>().hasFocus = false;
                    if (dis < switcherDis) {
                        switcherMin = swatch;
                        switcherDis = dis;
                    }
                }
                switcherMin.GetComponent<SwitchController>().hasFocus = true;
                collisions.switcher = true;
            }
            //check death hit
            if (hit_death) {
                collisions.death = true;
            }
		}
	}

    

    public void UpdateRaycastOrigins() {
		Bounds bounds = collider_check.bounds;
		bounds.Expand (skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}

	public void CalculateRaySpacing() {
		Bounds bounds = collider_check.bounds;
		bounds.Expand (skinWidth * -2);

		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;
        public bool coin;
        public bool enemy;
        public bool end;
        public bool death;
        public bool switcher;
        public int switcher_value;

		public void Reset() {
			above = below = false;
			left = right = false;
            coin = false;
            enemy = false;
            end = false;
            death = false;
            switcher = false;
            switcher_value = 0;
        }
	}

}
