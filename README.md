# TextureManipulationUtilities

This package is a compilation of high-performance utilities that speed-up some common texture tasks and let you manipulate texture channels, without needing an external program.

## Features implemented

*Combining an RGB texture with an alpha texture (Jobs support).
*Creating an HDRP mask map, based on a tool created by /u/Diabolickal (Job support in development).
*Inverting a channel. Very useful for DirectX<->OpenGL normal maps and for creating texture variation.

## Requirements

TMU doesn't have any requirements, but if you want to use the *Job System* support (very recommended), you need install "Jobs" and "Burst" from the package manager. You may need to enable "Enable preview packages" in Window->Package Manager->Advanced.