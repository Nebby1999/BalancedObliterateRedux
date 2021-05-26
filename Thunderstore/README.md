# Balanced Obliterate Redux
- A Continuation of MisterName's original [Balanced Obliterate](https://thunderstore.io/package/mistername/BalancedObliterate/) Mod.
- Gives extra lunar coins rewards when obliterating at A Momemt: Fractured's Obelisk.
- Amount of Lunar Coins based off a math equation using the amount of stages beaten, loops completed & difficulty multiplier.
- Has innate support for [Starstorm 2](https://thunderstore.io/package/TeamMoonstorm/Starstorm2/)'s Typhoon Difficulty.
- Has fallbacks for other mod's Difficulties, using the Rainstorm multiplier. (No longer will you not get survivor awards for obliterating with the original mod.)
- Mod Mildly Configurable. including options for the difficulty multipliers and amount of lunar coins per stage beaten.

## The Rewards System.
- The mod determines the amount of extra lunar coins to give to each player with a math Equation.
- The formula goes as follows...
![](https://media.discordapp.net/attachments/570060692414267397/846503810369454080/Untitled.png?width=1440&height=569)

### Coins per Stage Beaten
- As the name suggests, this is the amount of coins you recieve per stage beaten.
- By default, this value is set to 0.25, but you can change the amount in the config file.

### Stages Cleared
- The amount of stages cleared by the player.
- Encourages the player to go beyond Stage 8 by deducting the first 5 stages of the game. (penalty can be removed in the config file via a boolean switch)

### Loops Cleared
- The amount of loops completed by the player.
- Encourages the player to do long runs and attempt to create god runs, since the more loops you do, the higher the reward will be in the end.

### Difficulty Multiplier
- The difficulty Multiplier is determined by the difficulty you choose to play in.
- Has support for RoR2's 3 default difficulties, alongside Starstorm's Typhoon Difficulty.

Default Multiplier Values:

    - 1.0 for Drizzle
    - 1.5 for Rainstorm
    - 2.0 for Monsoon
    - 2.5 for Typhoon

The multiplier values are fully configurable in the config file.

In the original Balanced Obliterate, there was an error whenever you attempted to obliterate in a non vanilla difficulty, this error caused you to not gain neither extra lunar coins neither certain modded survivors' rewards. to fix this, Balanced Obliterate Redux has a fallback when encountering a custom difficulty index, causing the multiplier to be set to the Rainstorm Multiplier.

## Example Results

    > These example results are using the 2.5 multiplier given to the Typhoon Difficulty. and the default Coins per Stage value alongside the -5 penalty.

- Obliterating at stage 8.

![](https://media.discordapp.net/attachments/753709254803980296/846493609179217980/ce01390d7b5be917223b37a991b135e4.png)

- Obliterating at Stage 23

![](https://media.discordapp.net/attachments/753709254803980296/846494684267413514/6337caf75f8f60f42f651b095d5cba3c.png)

## Changelog
'1.0.1'

- Fixed the mod not working correctly if Starstorm wasnt installed.

'1.0.0'

- Initial Release