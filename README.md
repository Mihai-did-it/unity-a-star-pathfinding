# Unity A* Pathfinding Project

A Unity C# project that implements the **A\*** pathfinding algorithm within a custom framework.  
Built for exploring graph-based navigation, path cost evaluation, and optimal route discovery in game environments.

---

## Overview
This project provides the structure for implementing A* search over a grid or node-based map.  
The system evaluates nodes using `f(n) = g(n) + h(n)` and reconstructs paths dynamically between start and goal points.

---

## Features
- Modular A* implementation written in C#
- Node graph generation and neighbor detection
- Path cost calculation using heuristic and distance
- Integration-ready for Unity game objects
- Easily adaptable for 2D or 3D environments