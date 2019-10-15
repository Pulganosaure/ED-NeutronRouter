# ED-NeutronRouter
Small plugin for voice attack using the spansh.co API to create some neutron routes for Elite: Dangerous

spansh.co website : https://www.spansh.co.uk/plotter (thx for this beautiful tool)


# Variables : 

- Input Variables : 
  - {TXT: System name}
  - {TXT: Next system name}
  - {DEC: Jump range}
  
- Output Variables : 
  - {TXT:Previous system}
  - {TXT:Next system}
  - {INT:Remaining waypoints}
  - {INT:Remaining jumps}
  
# Contexts :
- clearRoute
- remainingJumps
- remainingWaypoints
- nextSystem
- previousSystem
- setRoute
- website
- getJumpRange (require EDDI)



# How it work ? 
## (EDDI plugin)
- Start VoiceAttack, Login into the game. OR jump into another system to update EDDI variables.
- set your Jump range manually or use the getJumpRange context.
      (or catch the Jump range value from the [EDDI Loadout event])(https://github.com/EDCD/EDDI/wiki/Ship-loadout-event)
- Open galaxy Map.
- Target a system (don't plot a route, just target it).
- execute your command with plugin context : "setRoute".

# How it work ?
## (without EDDI plugin)
- Set your variables:
    {TXT: System name}, 
    {TXT: Next system name}, 
    {DEC: Jump range}.
- execute your command with plugin context : "setRoute".

## about the getJumpRange context
***(not verified)** will do some test before validating this part. but if the plugin respond a error min range < 10 ly, it meen the value is at 0.*
EDDI store your ships fits in a file, each time you edit one of those, EDDI reset max Jump Range of your ship fit. you need to do do a jump at max range to set the jump Range variable.

# Installation:
 - download EDDI and follow the installation guide : https://github.com/EDCD/EDDI/releases 
 - download release : https://github.com/sc-pulgan/ED-NeutronRouter/releases.
 - Extracts Datas.
 - put the EDN-Router Folder into the Voice Attack App folder.
 - load the profil.vap from Voice Attack.
