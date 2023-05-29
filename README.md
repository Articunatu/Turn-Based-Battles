# Turn-Based Battles
A turn-based battle simulator game, with gameplay heavily inspired by the battle system from a certain RPG video game series. The key selling point of this genre version is to eliminate all forms of randomness from the battles, making it more competitive.

## Gameplay  
Two teams consisting of 5-6 characters duke it out against one other. One character from each team is used, during each turn. Access to 4 attacks and the option to swap with another character on their team.<br /><br />
The damage is calculated based on the stats, attacks, and elements of the characters. However, in this game, a favorable matchup may only result in a damage multiplier of 50% compared to the inspiration.<br />
Each character begins with 3 energy points, which are utilized as the cost for attacking. Each attack has a cost stat that deducts from the character's energy points. At the end of each turn, in which a character performed an action, their energy is increased by 1 (unless their energy value is already at the maximum of 3).<br /><br />
The faster character will be the first to utilize its attack, unless the slower character uses an attack with a higher attack speed than the faster character's attack. Alternatively, there is a possibility of modifying the mechanics so that attack speed acts as a multiplier to the character's agility, and the product determines the turn order.
If both characters and their attacks have equal speed, an attack collision occurs. Firstly, the power of each attack is calculated, followed by checking the elemental matchups. Finally, a comparison is made to determine which attack emerges as the winner. The remaining damage from the winning attack is then calculated and deducted from the losing character's health, while also applying any optional effects associated with the attack.

### Elements and matchup chart
![Elemental Matchup Chart](https://raw.githubusercontent.com/Articunatu/Turn-Based-Battles/main/elements.png)

## Code  
* Scriptable objects
* State machine pattern
* Animation: DOTWeen package
* Networking: Photon
