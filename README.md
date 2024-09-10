# unity-interaction-system
System for having players interact with other objects when interaction point triggers colliders with objects that are interactable.


IInteractable - Interface for tagging an object as Interactable

Interactor - Attaches to player and uses a trigger collider overlap with objects that implement the Interactable interface; used for starting and stopping interactions.

Types - Contains a variety of interaction types
-Switch On/Off (useful for lights or other buttons which are a true/false toggle)
-Door (handles the opening and closing of doors; rotation included)
-Character (for starting dialogue or interacting with NPC Actors)
-Chair (Locks player to object position to sit; unfinished, requires animation, transform tweaking, and exit state).

UI - For handling interaction UI prompts
Interaction Prompt UI - Displays UI in world space that shows the Interactor is overlapping with an interactable object, the hotkey for interacting 'e', and the key/interaction prompt of the interaction (i.e. [e] Talk)
