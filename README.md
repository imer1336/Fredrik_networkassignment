# Fredrik_networkassignment

Network Manager and Player Prefab:

I created a Network Manager and added the essential scripts: "Network Manager" and "Unity Transport".

Then, I registered a player prefab in the Network Manager so that the network would recognize it.

Syncing Player Positions:

I attached the Network Object script and found a handy Client Transform Script (ClientAuthorityManager) online.

This script was crucial for keeping player positions in sync between the host and client. I applied it to all network objects that needed to move.

Player Controller Setup:

I implemented an OnNetworkSpawn function, which is like the Start function but for network initialization.

I ensured each player had their own camera controlling the mouse coordinates.

Mouse input logic was added so the player would rotate toward the mouse, making fireball direction setup easier.

I decided the player should only rotate toward the mouse when holding down the W key, for better control.

Fireball Mechanic:

I added an invisible object in front of the player, called FireBallDirection, which served as the spawn point for fireballs.

Fireballs were converted into network objects, and I applied the "Network Transport" script to them.

Fireball spawning over the network was managed using a ServerRpc function.

Creating IceCubes:

To give the player something to shoot at, I created IceCubes that spawn off-screen and move toward the center.

I set up an InvokeRepeating function to spawn IceCubes every two seconds.

I created a helper function, GetRandomSpawnPosition, to calculate random spawn points just outside the visible screen area.

Collision Detection:

I added a simple collision detection system where fireballs destroy IceCubes, and IceCubes break upon hitting the player.

Emote Feature:

To enhance player communication, I added an emote feature using ClientRpc.



Struggles and Solutions



Multiple Fireballs:

Struggle: Multiple fireballs spawning at once.

Solution: Ensured only the host could update or destroy objects to prevent errors from clients trying to destroy objects they didn't have permission to.

Network Spawning Issues:

Struggle: Trouble with spawning IceCubes over the network.

Solution: Realized the IceCube spawner needed to be managed by the server. Due to time constraints, I opted for a quick fix by properly initializing the spawner on the network.

Packet Loss and Delay:

Struggle: Packet loss and delay when firing fireballs.

Solution: Identified the Interpolate function in the Network Transform as the cause. Considered adding a slight delay before destroying objects to help with synchronization.
