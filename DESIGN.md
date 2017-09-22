# Udacity Teamworks - Team Goodwin 
## Design document 

### Elevator pitch: 

"Splatoon, as a Job Simulator, meets Google Earth"

### Backstory: 

It's the distant future, robot AIs have developed a sense of nostalgia and a fashion for replicating the wonders of a long faded human society. They also really like pretty colours. 

As a painter droid in sector 7G, your job is to re-color some of the great landmarks of the human world - and fast, there are plenty more to get through!

### Platform:

We've chosen to make the game for mobile VR (iOS Cardboard), to make it more accessible and to force us to keep the scope simple. This limits the complexity of the environment model and also the controls but keeps it a casual playing experience. It also the only environment consistently available to the whole team.

### Game idea:

Basically a first person shooter but with a paint gun and an environment that you are bringing back to its former glory, with colour. It combines exploring an interesting environment, i.e. wonders of the world, famous landmarks, with play - firing a gun, fixing an environment.

There is a puzzle element as you can see a queue of paint colours coming up, so you know where to aim next, where to position yourself but the majority of the gameplay is based on colouring the environment correctly, as quickly as you can and as accurately as you can. 

### Mechanics

#### Paint Gun

On the Cardboard, the gun is triggered by button press. It fires a paint bullet in a slightly arced movement from the center of the player's view. The paint bullet colours environmental objects that it hits. 

#### Moving around 

For mobile, we will restrict movement to 3dof with waypoints to move around the environment. On the Cardboard, the gun will be triggered by button press, so waypoints will be triggered by gaze control. Movement will be by 'dash forward' as it feels more like being in the environment. 

#### Painting

The paint gun will transform a totally desaturated environment in to a full colour 'wonder of the world'.

For the demo, this means firing flat colours at parts of the environment that will fill with colour if the environment 
 
### Options for extension

The game levels are based on real world landmarks. The demo has one level - St Basil's Cathedral, Moscow, Russia - as the location was suitably colourful, interesting and beautiful. 

On a practical level, there were also free models available of this landmark that could be reduced in complexity and made into game ready assets. 

To extend the game, we would have a variety of landmarks of differing complexity. 

We could also add various improvements to paint gun (mini-gun paintgun, paint grenades).  

#### Wireframe / UX Design

START - UI single button to start game (early preview of paint gun mechanic?)

LEVEL PREVIEW - show image of what the landmark should look like coloured in. Image should be available throughout level to get colour clues. (Timer then reveal PLAYABLE LEVEL)

PLAYABLE LEVEL - simple plane with landmark and waypoints to move around, paint gun to fire colours at landmark. UI - show queued colours near gun, gaze down at floor to pause, exit game and show picture of landmark.

LEVEL COMPLETE - stats for time taken, paint bullets fired. UI - restart level (takes you back to LEVEL PREVIEW) or exit level (takes you back to START).