# Jump demo
Demonstration of a versitile jumping implementation in a 2D-platformer-game.

## Jumping in platformers
In platformers it is not enough to simply add force upwards in order to simulate a jump. A jump that "looks" good does not necessarily "feel" good in terms
of player experience. Relying on forces also makes it so that jump-height <i>may</i> be affected by other velocity calculations, and therefore be inconsistent.
<br></br>
The player needs to know exactly how high they can jump.
