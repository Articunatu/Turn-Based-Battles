# Turn-Based Battles
A turn-based battle simulator game, with gameplay majorly inspired by the battle system from a certain RPG video game series. The key selling point of this version of the genre is to remove all types of randomness from the battles, in order to make it more competitive. 

## Gameplay  
Two teams consisting of 5-6 characters duke it out against one other. One character from each team is used, during each turn. Access to 4 attacks and the option to swap with another character on their team.<br />
The damage is calculated by the stats of the characters and attacks and also the elements of the characters and attacks. However, a favorable matchup may only result in a damage multiplier of 50% in this game, in comparison to the inspiration.<br />
Each character starts with 3 energy points, which are used as a cost for attacking. Every attack has a cost stat which is subtracted from the character’s points. During the end of each turn - in which a character performed an action - their energy is increased by 1 (unless the value of their energy is maxed out at 3).<br />
The faster character will be the first to use its attack unless the slower one uses an attack with a higher attack speed than the faster one’s attack. (this might be changed so that the attack speed only works as a multiplier to the character's agility, and the product decides who goes first).
If both characters and their attacks are equally fast, the attacks will result in an attack collision: First, the power of each attack is calculated, then the elemental matchups are checked, to finally compare which attack wins. The rest damage of the winning attack is calculated and subtracted from the losing character’s health, as well as applying optional effects of the attack.


## Code  
* Scriptable Objects
* State Machine
* Animation
* Networking 
