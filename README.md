# First Person Camera Project

TODO list:
- CONTINUE LATER: a simple 3D world with cuboids and cylinders
- DONE: a camera can move around in 3D
- DONE: the camera can't intersect with other objects
- DONE: the camera can be rotated freely
- DONE: the world is made of "pickable" and un-"pickable" objects
- if the camera is looking at a "pickable" object and the object is close enough, it gets highlighted/outlined (if nothing is being held right now)
- if the spacebar is pressed when nothing is held and an object is highlighted, the highlighted object is "picked up": forces are applied to it to move it towards a point that is a fixed distance in front of the camera
- as the camera is moved around / rotated, forces are applied to the object to keep it at that point relative to the camera
- if the "held" object is too far from that point (e.g. because there's a wall or another object in the way), it is "dropped"
- if the spacebar is pressed while holding an object, the object is dropped.
- if Q is pressed while holding an object, the object is thrown forward
