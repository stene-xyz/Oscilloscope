# Oscilloscope
Version 1.0.0 by stene.xyz (johnny@stene.xyz)

## About
This is about as simple as a DIY oscilloscope can get. To make it, you
need:
- 1 Arduino (I used a Nano)
- 1 100k resistor
- 1 10k resistor
- 1 alligator clip (to connect GND)
- 1 probe (to probe measurement points)

The schematic is available in `Oscilloscope.ino`.
You'll need to build ScopeView (the companion software) using a copy of Godot (the C# version), or 
write your own viewing software. This is quite simple, as the scope just outputs 
the voltage value as strings over serial.

## TODO
- Add some sort of display, possibly with a touchscreen
- Port to the RP2040
