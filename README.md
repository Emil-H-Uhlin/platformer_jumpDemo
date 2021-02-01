# Jump demo
Demonstration of a <i>reusable</i> and <i>modifiable</i> jumping implementation in a 2D-platformer-game.

## Purpose
The purpose of this project was to create a jumping script that can be reused in many platforming games by simply creating new jump-settings that affect the jumps look and feel. I've made a number of platforming games on my spare time where I spent alot of time implementing jumping that almost always turns out the same way.

In this implementation I've focused on <b>reusability</b> and <b>modifiability</b> (which further strengthens <i>reusability</i>).

A playable <b>browser demo</b> is available at <a href="https://emil-h-uhlin.github.io/platformer_jumpDemo/" title="Playable demo">emil-h-uhlin.github.io/platformer_jumpDemo</a>.

## Jumping in platformers
It is simply not enough to add force upwards and call it a day. A jump that "looks" good (or realistic) does not necessarily "feel" good. Relying on forces makes designer input difficult since a value of say <i>17.5 units</i> doesn't quite describe how high, how far or for how long the character will be jumping. Every value needs to be tested. Relying on forces may also result in inconsistent jumps, as any number of forces can affect the character at any given time. 

## Implementation
In my implementation I use ScriptableObjects as JumpSettings in order to store different types of jumps in separate files. These can be used for different levels, areas or characters. 

My implementation allows for linear, non-linear (or quadratic) jumps with pre-defined <i>jump height</i>, as well as <i>minimum jump height</i> - allowing for shorter jumps with release of the jump-key. I also implemented a variable for <i>up-time</i> that defines how <i>long</i> it takes for the character to get to the max height. There's also an implemented <i>hover-time</i> variable that defines for how long the player can hold the jump key at max height to keep hovering. <br></br>
