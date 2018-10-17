Team Members: Christian Lee, Zhangliang Ma


Build can be found in the Builds folder.

Press 1,2,3 to switch between/reset parts. 1 leads to Part 1, etc.

***Part 1***

Controls:
Move player - Arrow keys or WASD or press and hold with mouse

1) What are the weights of the three steering behaviors in your flocking model?

Separation - 3
Cohesion(Arrive) - 0.5
Velocity Match- 1
Pursue - 2
Face - 1

- The Boids follow the player using Pursue.
- Pursue was set to 2 so that the Boids can keep up with the player and emphasize
the flocking behavior.
- Face is the only behavior that affects orientation, so the weight is set to 1.
- Separation is set to 3 with a decay coefficient of 10 to make the Boids more spread out.
- Cohesion is set to 0.5 so that the velocity match is more effective.
- Background is colored as a gradient to make the movement more clear.
- Align was originally added so that the Boids had a similar orientation to its group,
but that made the Boids turn unnaturally.


***Part 2***

Controls:
Switch to Cone Check - Q
Switch to Collision Prediction - E
Move Camera Horizontally - AD or Left and Right Arrows

2) In Part 2, what did you do for avoiding a group of agents? What are the weights of
path following and evade behavior? Did you use a separation algorithm, and what
were its parameters?

To avoid a group of agents, the Cone Check or CollisionPrediction algorithms were added
as extra behaviors for Flock. The Cone Check does the dot product calculation, then calls
Separate on the target. For multiple targets, the cone check only calls Separate on the closest one.

Cone Check: Threshold = 2
Collision Prediction: Radius = 1

- An invisible lead uses the Path Following behavior to follow a path.
- The Flocking behavior calls Pursue on this lead. This allows the Boids to follow the path
as a group
- The Flocking behaviors and weights are:

Separate - 3
Arrive - 0.5
Velocity Match - 1
Pursue - 2
Face - 1
Cone Check- 2
Collision Prediction - 1

- The last two behaviors are toggled false, but can be set to true using Q or E.
- Even with the collision prevention behaviors, the Boids tend to collide with each other a lot.
- The Collision Prediction behavior seems to perform better than the Cone Check behavior.

***Part 3***

Controls:
Move Camera Horizontally - AD or Left and Right Arrows

3) In Part 3, how many rays did you use in your ray-casting, and why?

Three rays were used in the ray-casting. The first ray points forward and has
the largest length. The second and third ray points almost 45 degrees from the first
ray. Originally, one ray was used but that made the boid get stuck on the corner turn
at location 4. Three rays fixes this error and provides a way to detect obstacles on the
sides of the boid.

Avoid Distance - 2
Lookahead - 3