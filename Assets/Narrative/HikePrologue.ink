VAR scout_confidence = 10

// Divert root execution straight into the start knot
-> start

=== start ===
Milo: *Sniffs the split in the trail, ears twitching forward.* [Fork ahead. Evaluate the safer route.]
Hiker_1: What do you smell up there, Milo? Which way should we go?
* [Bark toward High Ridge Trail] -> high_ridge
* [Nudge toward Stream Pass] -> stream_pass

=== high_ridge ===
~ scout_confidence = scout_confidence + 2
Milo: *Gives two sharp, confident barks and steps onto the upper rock shelf.* [High Ridge selected: Stable granite, steeper climb.]
Hiker_2: Ridge trail it is. Keep your balance on the incline!
-> END

=== stream_pass ===
~ scout_confidence = scout_confidence - 1
Milo: *Whines softly, pawing near the muddy edge.* [Stream Pass selected: Level ground, slippery mud.]
Hiker_1: Looks pretty wet down there, but let's follow Milo's lead.
-> END