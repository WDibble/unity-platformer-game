# 🎮 Into The Shadows - A Strategic 2D Platformer

<div align="center">
![Game Preview](/src/assets/preview.png)

A thrilling 2D platformer where mastering invisibility and strategic bullet choices are your keys to survival.

[![Download](https://img.shields.io/badge/Demo-Play%20Now-red.svg)](https://thedibbs.itch.io/into-the-shadows)
[![Made with Unity](https://img.shields.io/badge/MadewithUnity-57b9d3.svg)](https://unity.com/)
[![C#](https://img.shields.io/badge/Language-C%23-239120.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![A* Pathfinding](https://img.shields.io/badge/Pathfinding-A*-yellow.svg)](https://arongranberg.com/astar/)
[![Game Dev Portfolio](https://img.shields.io/badge/Portfolio-GameDev-blueviolet.svg)](https://willdibble.com/)
</div>

## 🎯 Game Overview
Into The Shadows combines classic platforming challenges with unique invisibility mechanics and a diverse bullet system. Players must navigate through increasingly difficult levels while managing their invisibility power, collecting different types of ammunition, and facing off against intelligent enemies.

## ✨ Key Features

### 🕹️ Core Mechanics
- **Invisibility System**: Strategic invisibility usage reveals hidden platforms and affects enemy behavior
- **Bullet Types**:
    - 🟧 Orange Bullet: Reliable and consistent
    - 🟦 Blue Bullet: High damage, short range
    - 💗 Pink Bullet: Arcing trajectory, maximum damage
- **Double Jump**: Unlocked in Level 3
- **Timer-based Scoring**: Challenge yourself to beat your best times

### 🎨 Level Design
- 4 Unique Levels with increasing difficulty
- **Hazard Types**:
    - ⚔️ Static Spikes
    - 💫 Moving Sawblades
    - 💥 Breakable Platforms
    - 🧨 TNT-dropping enemies
    - 👻 Visibility-dependent platforms

### 🤖 Enemy AI
- **Enemy Types**:
    - Jumping Enemies with calculated trajectories
    - Ground-based Rolling Enemies
    - Aerial TNT-dropping enemies
    - Final Boss Bat with advanced AI
- **Dynamic Difficulty**: Enemy behavior adapts to player visibility

### 🎵 Audio Design
Custom sound effects pack
Dynamic music system
Level-specific atmospheric tracks
Smooth audio transitions

## 🎮 Controls
```
Movement: Arrow Keys / WASD
Jump: Spacebar
Shoot: C
Switch Weapon: V
```

## 🛠️ Technical Implementation
### Core Systems
- [Weapon.cs](Assets\Scripts\Weapon.cs): Handles weapon switching and bullet firing
- [PlayerMovement.cs](Assets\Scripts\PlayerMovement.cs): Controls player physics and animations
- [BatEnemyAI.cs](Assets\Scripts\BatEnemyAI.cs): Implements A* pathfinding for boss AI
- [AudioManager.cs](Assets\Scripts\AudioManager.cs): Manages dynamic audio system

### Enemy AI
- A* Pathfinding for intelligent enemy movement
- State-based behavior systems
- Visibility-reactive AI adjustments

### Performance Optimizations
- Efficient bullet pooling system
- Optimized collision detection
- Smart enemy spawning mechanics

## 🎨 Art Assets
- Custom character animations
- Particle effects for impacts
- Environmental assets
- UI elements and HUD

## 🎵 Audio Credits
- Background Music:
- "Relaxing" by Music For Videos
- "Dreamy Space Soundtrack" by Cactusdude
- "Good Day To Die" by Miguel Johnson
- Sound Effects: JDWasabi's Sound Effects Pack
- Additional Effects: "Large Wings Flapping Foley" by ToTrec2

## 🔧 Development Tools
- Unity 2D
- C# Programming
- A* Pathfinding Project
- Visual Studio Code
- Git Version Control

## 👨‍💻 Development Team
William Dibble - Game Developer, Designer & Programmer

## 📝 License
This project is protected under copyright law. All rights reserved.