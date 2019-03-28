# ED-NeutronRouter
Small plugin for voice attack using the spash.co API to create some neutron route for Elite: Dangerous


# Variables : 

- Input Variables : 
  - {TXT: System name}
  - {TXT: Next system name}
  - {DEC: Jump range}
  
- Output Variables : 
  - {TXT:previousSystem}
  - {TXT:nextSystem}
  - {INT:remaining Waypoints}
  - {INT:remaining Jumps}
  
# Contexts :
- clearRoute
- remainingJumps
- remainingWaypoints
- nextSystem
- previousSystem
- setRoute

# How it work ? 
## (EDDI plugin)
- Start VoiceAttack, Login into the game. OR jump into another system to update EDDI variables.
- Set your jump range into the {DEC: Jump range} variable.
- Open galaxy Map.
- Target a system (don't plot a route, just target it).
- execute your command with plugin context : "setRoute".

# How it work ?
## (without EDDI plugin)
- Set your variables:
    {TXT: System name}, 
    {TXT: Next system name}, 
    {DEC: Jump range}.
- Open galaxy Map.
- Target a system (don't plot a route, just target it).
- execute your command with plugin context : "setRoute".

# Installation:
 - download release : https://github.com/sc-pulgan/ED-NeutronRouter/releases.
 - Extracts Datas.
 - put the EDN-Router Folder into the Voice Attack App folder.
 - load the profil.vap from Voice Attack.
