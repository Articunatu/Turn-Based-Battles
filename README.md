# Turn-Based Battles

## Gameplay  


Two teams consisting of 5-6 characters duke it out against one other. One character from each team is used, during each turn. Access to 4 attacks and the option to swap with another character on their team. 
The damage is calculated by the stats of the characters and attacks and also the elements of the characters and attacks. However, a favorable matchup may only result in a damage multiplier of 50% in this game, in comparison to the inspiration.
Each character starts with 3 energy points, which are used as a cost for attacking. Every attack has a cost stat which is subtracted from the character’s points. During the end of each turn - in which a character performed an action - their energy is increased by 1 (unless the value of their energy is maxed out at 3).
The faster character will be the first to use its attack unless the slower one uses an attack with a higher attack speed than the faster one’s attack. (this might be changed so that the attack speed only works as a multiplier to the character's agility, and the product decides who goes first).
If both characters and their attacks are equally fast, the attacks will result in an attack collision: First, the power of each attack is calculated, then the elemental matchups are checked, to finally compare which attack wins. The rest damage of the winning attack is calculated and subtracted from the losing character’s health, as well as applying optional effects of the attack.

**Elements**  
In this matchup chart all the 11 elements are displayed: (Rows = Attackers, Columns = Defenders)
![Elemental matchup chart]()

**Attack**:  
Primary action be a character; can be used to deal damage to opposing characters, change your attribute values, and other effects. Just like a character, an attack can have an element (but only 1).

**Effect - Equipment:**  
Any character can wear any one set of equipment, which each comes with unique effects. These effects can either be permanent or one-time-use.

**Effect - Ability:**  
Every character has one ability, which grants some sort of effect during the battle. When choosing attributes and attacks for your character, you will have at least two abilities to choose from. If you're creating a new character from scratch the abilities will be decided by an algorithm taking into consideration the balance of attributes, attack strengths, etc.

**Character Stats:**  
Health - 
Strength - When the used attack is of usage category “movement”
Toughness - When an incoming attack is of the damage category “force”
Magic - When the usage category is not “movement”
When the damage category is not “force”
Velocity - Determines which character gets to attack first
Energy - 
Primary Element - Mandatory element, used for calculating the damage of receiving attacks
Secondary Element - Same as above, but not required for a character to have

**Attack Stats**  
Damage
Cost
Speed 
Effects
Damage Category - Force or heat
Usage Category - Movement or stationary

**Terminology**  
Recoil: Damage a character does to itself when using certain attacks or abilities
Heal: Restore an amount of a character’s health
Hazard: Damage or effect that occurs when a character enters / swaps into the battle
Area: 
Recharge: A character cannot use an attack during the next turn and the rest of the current one
Raise: Add points to a stat
Lower: Subtract points from a stat


**Win Condition**:  
When your opponent has no more usable characters, you win!


**Characters**  

  
In the first release of the game, there should be around 12 characters to choose from. These should all be newly designed characters based on a couple of animals.
In the next iteration, a player should have the option to combine an animal with whatever element they want (max 2 elements as usual) in order to create a “new species”.  



## Code  
* Scriptable Objects
* State Machine
* Animation
* Networking 
