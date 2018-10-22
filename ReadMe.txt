Team Members: Christian Lee, Zhangliang Ma


Build can be found in the Builds folder.

Controls:
Move player - Arrow keys or WASD or press and hold with mouse
Press 1,2,3 to switch between/reset formations.

Scalable Formation:
- A script creates a formation as a list of point objects.
- The scalable level manager assigns one of the twelve boids closest to the spawn point
as the leader.
- The leader will Path Follow through the path and avoid obstacles when needed.
- The point objects are set to the child of the leader so that they move around with the leader.
- Each point is assigned to a Boid that is not the leader.
- These follower Boids call the Arrive and Align behaviors to copy the formation around the leader.
- Originally, the position and the orientation of the follower Boids would be set directly on the
point, but this would lead to jittery movement and problems when deleting Boids. Instead, behaviors
were added.
- The raycasts for this formation extended through the entire group, simulating a raycast for one
big Boid.

Emergent Formation:
- The first spawned Boid is set as the leader and follows the same behavior as in Scalable.
- Each follower then chooses as its target the closest Boid that target chains to the leader
including the leader itself. This will make sure that every Boid is connected to the leader.
- The follower Boids call the Arrive, FaceForward, and Separate behaviors.

Two Level Formation:
- The leader is set as an intangible and invisible Boid that calls the Path Follow behavior.
- The followers call the Arrive behavior on the formation Points and various other behaviors
for general movement.

1) What did you use for obstacle avoidance?

A raycast configuration was used for obstacle avoidance. Four raycasts were used overall.
The first two raycast are set to the sides of the Boid or Boid group and are parallel to the
Boid's velocity. The other two raycasts come from the center of the Boid or Boid group and
move out 45 degrees from the velocity.
The parallel rays determine which direction the Boids should turn while the angled rays are used
to determine if the Boid is parallel to a wall and should continue moving.

2) What are the heuristics for the agent to go through the tunnels?

A script was added to the leader of each formation. This script added three rays on the leader.
One ray pointed forward while the other two pointed at an angle. These rays acted as a trigger
such that if the center ray is not colliding anything and the other two are, the follower boids
would change to the Path Following behavior. Once the follower Boid goes past the obstacle, it
changes back to the regular behavior.

3) Did you use any additional heuristics?
For Scalable, another trigger was added so that once obstacle 4 is passed, the
group sized raycast would disappear and the formation size would become smaller. If this was not done,
then the leader Boid would not follow the path since the gap in obstacle 6 would trigger the Raycast
behavior. The Follower Boids would also not go through this gap since it was so big.

4) What are the differences in the three groups' performances?

The scalable formation is the most consistent out of the three. It was also the hardest one to format
so that it could pass the obstacle course. It required the most heuristics to get past each obstacle
and took the longest to implement of the three.

The emergent formation is the least consistent of the three, but also the easiest to implement. Since
the Boids follow a tree-like formation, they can either clear the obstacles extremely easily, or get split
from the formation and wander around the obstacles. The formation is the most flexible of the three,
so not many heuristics were needed. The formation will usually pass the course, but there may be times
where the Boids completely fail even the first obstacle.

The two-level formation is also fairly easy to implement since the leader never needs to change. Since
it follows the same formation as the scalable, it does require some heuristics to complete some of the
obstacles. The tunnel obstacle proved to be the most challenging for this formation since the Boids are
not as reliable as those from the scalable. Since the leader does not have to move through walls, 
obstacle avoidance was the easiest to complete. The Boids would not get as stuck as in the emergent formation,
and the leader itself would not get stuck.