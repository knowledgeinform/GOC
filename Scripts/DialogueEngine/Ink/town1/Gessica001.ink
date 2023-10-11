-> main

=== main ===
#speaker:Gessica
Hey there, <&player>! Are you here to begin your training?
 + [Yes!]
    -> yes
 + [Maybe later...]
    -> no

=== yes ===
#scene:LabScene
Wonderful! There's a lot to cover, so let's get going.
-> END

=== no ===
Okay! Feel free to come back when you're ready.
-> END