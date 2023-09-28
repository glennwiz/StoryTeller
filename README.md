
# RnD project in C# cli and discord boot for LLM inference

currently supports gguf models 

![img.png](img.png)

![image](https://github.com/glennwiz/StoryTeller/assets/195927/1f23cbf7-5e76-445e-b231-aedd213b5712)


# Summary of `InferenceParams` Class Fields

## General Parameters

### TokensKeep
- **Description**: Specifies how many tokens from the initial prompt should be retained.
- **Default**: `0`

### MaxTokens
- **Description**: Maximum number of new tokens to predict.
- **Default**: `-1` (infinite until completion)

### LogitBias
- **Description**: A dictionary mapping specific tokens to their logit biases.
- **Default**: `null`

### AntiPrompts
- **Description**: Sequences where generation stops.
- **Default**: Empty array

### PathSession
- **Description**: File path for saving/loading model eval state.
- **Default**: Empty string

## Input Formatting

### InputSuffix
- **Description**: Suffix to add to user inputs.
- **Default**: Empty string

### InputPrefix
- **Description**: Prefix to add to user inputs.
- **Default**: Empty string

## Token Selection

### TopK
- **Description**: Number of most probable tokens to consider for generation.
- The topK parameter changes how the model selects tokens for output.
- A topK of 1 means the selected token is the most probable among all the tokens in the model’s vocabulary (also called greedy decoding),
  while a topK of 3 means that the next token is selected from among the 3 most probable using the temperature.
- For each token selection step, the topK tokens with the highest probabilities are sampled.
- Tokens are then further filtered based on topP with the final token selected using temperature sampling.
- **Default**: `40`

### TopP
- **Description**: Cumulative probability mass threshold.
- The topP parameter changes how the model selects tokens for output.
- Tokens are selected from the most to least probable until the sum of their probabilities equals the topP value.
- For example, if tokens A, B, and C have a probability of 0.3, 0.2, and 0.1 and the topP value is 0.5,
  then the model will select either A or B as the next token by using the temperature and exclude C as a candidate.
- The default topP value is 0.95
- **Default**: `0.95`

### TfsZ
- **Description**: Unknown (Documentation missing).
- **Default**: `1.0`

### TypicalP
- **Description**: Unknown (Documentation missing).
- **Default**: `1.0`

### Temperature
- **Description**: Controls randomness in token selection.
- The temperature controls the degree of randomness in token selection.
- The temperature is used for sampling during response generation, which occurs when topP and topK are applied.
- Lower temperatures are good for prompts that require a more deterministic/less open-ended response, while higher temperatures can lead to more diverse or creative results.
- A temperature of 0 is deterministic, meaning that the highest probability response is always selected.
- **Default**: `0.8`

## Penalties

### RepeatPenalty
- **Description**: Penalty for token repetition.
- **Default**: `1.1`

### RepeatLastTokensCount
- **Description**: Last n tokens to penalize.
- **Default**: `64`

### FrequencyPenalty
- **Description**: Coefficient for frequency penalty.
- **Default**: `0.0`

### PresencePenalty
- **Description**: Coefficient for presence penalty.
- **Default**: `0.0`

## Mirostat Parameters

### Mirostat
- **Description**: Algorithm type based on the paper [https://arxiv.org/abs/2007.14966](https://arxiv.org/abs/2007.14966).
- **Default**: `Disabled`

### MirostatTau
- **Description**: Target entropy for Mirostat.
- **Default**: `5.0`

### MirostatEta
- **Description**: Learning rate for Mirostat.
- **Default**: `0.1`

## Miscellaneous

### PenalizeNL
- **Description**: Consider newlines as repeatable tokens.
- **Default**: `true`
