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

A raycast configuration was used for obstacle avoidance.

2) What are the heuristics for the agent to go through the tunnels?


3) Did you use any additional heuristics?

No.

4) What are the differences in the three groups' performances?

