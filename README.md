# Jump demo
Demonstration of a <i>reusable</i> and <i>modifiable</i> jumping implementation in a 2D-platformer-game.

## Purpose
The purpose of this project was to create and demonstrate a jumping script that can be reused in many platforming games by simply creating new jump-settings that affect the jumps look and feel. I've made a number of platforming games on my spare time where I spent alot of time implementing jumping that almost always turns out the same way.

In this implementation I've focused on <b>reusability</b> and <b>modifiability</b> (which further strengthens <i>reusability</i>).

A playable <b>browser demo</b> is available at <a href="https://emil-h-uhlin.github.io/reusable-platformer-jump/" title="Playable demo">emil-h-uhlin.github.io/reusable-platformer-jump</a>.

## Background
It is simply not enough to add force upwards and call it a day. A jump that "looks" good (or realistic) does not necessarily "feel" good. Relying on forces makes designer input difficult since a value of say <i>17.5 units</i> (which is the default in the demo) doesn't quite describe how high, how far or for how long the character will be jumping. Every value needs to be tested. Relying on forces may also result in inconsistent jumps, as any number of forces can affect the character at any given time. 

## Implementation
In my implementation I use Unity's ScriptableObject class to create serialized JumpSettings. These can be used for different levels, areas or characters. 

My implementation allows for two main types of jumps (bear in mind they look very similar due to low framerate):
<details open>
  <summary>Linear jumps:</summary>
  
![linear jump](https://user-images.githubusercontent.com/45757491/116815186-b6f85100-ab5c-11eb-8e65-50fe3d4f5857.gif)
</details>

<details open>
<summary>Non-linear (or quadratic) jumps:</summary>
  
![quadratic jump](https://user-images.githubusercontent.com/45757491/116815226-e018e180-ab5c-11eb-806f-1ea38536748e.gif)
</details>

I also implemented a fully customizable jump for the purposes of this demo, in which the following properties can be changed: 
- <b>Jump height</b> <i>(full jump height)</i>
- <b>Minimum jump height</b> <i>(which allows for the player to release the jump key to fall to interrupt the jump)</i>
- <b>Up-time</b> <i>(the duration of a complete jump)</i>
- <b>Hover-time</b> <i>(the duration the character can remain at the max jump height by holding the jump button)</i>


https://user-images.githubusercontent.com/45757491/116815732-266f4000-ab5f-11eb-8099-8608ac8f6308.mp4
