# Roller1D

This is simplified version of Unity's [Making a New Learning Environment](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Learning-Environment-Create-New.md) tutorial.

## Controls

The ball (agent) can roll left or right on the x-axis. It uses force on the rigidbody to move the ball. So the agent has to deal with lateship.

## Rewards & Objective

The goal of the agent is to collect the box by rolling into it. This is rewarded with +1. Further more falling off the edge is to be avoided and gets punished with -1.

Additionally the environment rewards the agent for rolling towards the box by +0.1 per frame, where the distance gets smaller. Finally there is penalty for the time it takes by -0.05 per frame.

## Training Config

The agent was trained for 50.000 steps with the following config:

```
Roller1DBrain:
    normalize: false
    batch_size: 1024
    beta: 5.0e-3
    buffer_size: 10240
```

These values are copied from the BananaBrain example from Unity ML since this seems to be the closest.

## Result

The agent learned to avoid falling completly and collectes the boxes one by one reliable.

![Alt](./images/Roller1D.gif "Record of agent performance after training")