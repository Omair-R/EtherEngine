### Disclaimer
This project is far from being mature for release. These kinds of projects take a lot of time, which I sadly don't have much of in the meantime. However, I decided to make this repository public, as it might be of help to someone, perhaps as a reference of a sort. I might continue working on it in the future, but I can't make any promises.

# EtherEngine
EtherEngine is a game engine extension built on top of [Monogame](https://monogame.net/) (currently underconstruction). It follows the entity component system (ECS) architecture thanks to the wonderful [Arch](https://github.com/genaray/Arch). 

## Features
- Motion: several options for physics based motion, including simple massless motion, massless motion with realistic drag based on [Stoke's](https://en.wikipedia.org/wiki/Drag_(physics)#Low_Reynolds_numbers:_Stokes'_drag) and [quadratic](https://en.wikipedia.org/wiki/Drag_(physics)#At_high_velocity) drag, and mass-based motion with [PID](https://en.wikipedia.org/wiki/Proportional%E2%80%93integral%E2%80%93derivative_controller) contorlled physics.
- Collision: separating-axis-theorem-based collision for circles, rectangle (rotatable or AABB) and convex polygons. Provides ready events, point of contact detection, and collision layers.
- Sprites: Ether extends the sprite pipeline of monogames, abstracts it, adapts it to ECS and enables animation with atlas.
- Primative shapes: a simple drawing pipeline for primative shapes (rectangles, circles, and polygons).
- Camera: controllable camera with smooth motion enabled through [PID](https://en.wikipedia.org/wiki/Proportional%E2%80%93integral%E2%80%93derivative_controller) control.
- Particle system: a flexible particle emission system enabled by ECS.
- Entity relations: a relation system between entity, either in general form or as child-parent.
- Tweens: a tweening system for numrical values and colors (linear, ease in quad, ease out quad, ease in out quad).
- Random number generators (RNG): a wide variety of efficient RNG algorithms.
  - [Permuted congruential generator](https://en.wikipedia.org/wiki/Permuted_congruential_generator) (PCG).
  - [SplitMix64](https://dl.acm.org/doi/10.1145/2714064.2660195).
  - [Xoroshiro](https://prng.di.unimi.it/): Xoroshiro128Plus, Xoroshiro128PlusPlus, Xoroshiro128StarStar, Xoroshiro64Star, Xoroshiro64StarStar.
  - [Xorshift](https://en.wikipedia.org/wiki/Xorshift): Xorshift128Plus, Xorshift64Star.
  - [Xorwow](https://www.jstatsoft.org/article/view/v008i14).
- [LDTK] integration: converts LDTK levels to entities and components usable within Ether.
- [Dear ImGui] integration.
- Scene system, input management, timers...etc.

## Packages used in this engine
- [Arch](https://github.com/genaray/Arch) - 1.2.5
- [ImGui.NET]() - 1.90.8.1
- Microsoft.Extensions.ObjectPool - 8.0.5
- Newtonsoft.Json - 13.0.3
- Monogame (of course) - 3.8.1.303
