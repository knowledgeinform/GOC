-> main

=== main ===
#speaker:Shipmaster
'Ey there, <&player>, nice to see ye in here again.
What are ye here for today?

+ [Change ship color]
    -> choices
+ [Never mind!]
    -> cancel
    
=== cancel ===
Ah, alright then, laddie.
Best take care of yeself, yeh? Come back anytime.
    -> END
    
=== choices ===
Right then, which color would ye like today?

+ [Black]
    #shipColor:black
    -> changeShipColor("black")
+ [White]
    #shipColor:white
    -> changeShipColor("white")

=== changeShipColor(newColor) ===
So you want {newColor}, eh? Wise choice.
#speaker:SYS
Your ship color was updated to {newColor}!
#speaker:Shipmaster
Glad I could help. Come back anytime.
    -> END
