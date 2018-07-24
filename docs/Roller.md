# Roller

This environment is based of [Making a New Learning Environment](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Learning-Environment-Create-New.md). A simple Roll-a-Ball game where the objective is to collect a box by controlling a ball. The player should avoid falling off the edge.

## First Attempt

As a strating point I used the unmodified environment from the Unity tutorial and traind the agent for over 18 hours.

### Training Config

Two parameters where overriden for this experiment. The `max_steps` where increased to 5.0e6 to train for a much longer time. Additionally `beta` was reduced to 5.0e-4 to ensure the entropy going down over time.

```
RollerBrain:
    max_steps: 5.0e6
    beta: 5.0e-4

default:
    trainer: ppo
    batch_size: 1024
    beta: 5.0e-3
    buffer_size: 10240
    epsilon: 0.2
    gamma: 0.99
    hidden_units: 128
    lambd: 0.95
    learning_rate: 3.0e-4
    max_steps: 5.0e4
    memory_size: 256
    normalize: false
    num_epoch: 3
    num_layers: 2
    time_horizon: 64
    sequence_length: 64
    summary_freq: 1000
    use_recurrent: false
    use_curiosity: false
    curiosity_strength: 0.01
    curiosity_enc_size: 128
```

### Reward Function

The agent recives a reward of +1 for collecting the goal and -1 for falling of the platform. Additionally he is rewarded +0.1 for every frame he gets closer to target (box) and recived a time penalty of -0.05 each frame.

### Results

The agent learned to kill itself quickly to avoid accumulating negative rewards.

![Alt](./images/Roller_first_attempt.gif "Record of agent performance after training")

![Alt](./images/Roller_first_attempt_graphs.PNG "TensorBoard graphs of training session")