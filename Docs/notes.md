# Criação do jogo

# Place building

- &check; Buscar o valor do mouse no mundo do jogo
- &check; Cache camera
- &check; Criar sprite branco para debug
  - &check; Criar pasta Textures
  - &check; Adicionar global light 2D
- &check; Adicionar o Transform q será instanciado 
- &check; Criar WoodHarvester
  - &check; Adicionar dentro do WoodHarvester a um gameobject sprite para ter a referencia da imagem
  - &check; Adicionar esse recurso como Prefab 

# Building Types

- Criar ScriptableObject
  - Todos os ScriptableObjects serão herdados do tipo BuildingTypeSO
  - Adicionar annotation CreateAssetsMenu na classe para ela ser visível no editor
- Criar StoneHarvester ScriptableObject
- Criar uma classe separada para armazenar a lista dos objetos possíveis
- Importar a lista no BuildingManager
  - Criar pasta chamada Resources
  - `Resources.Load<BuildingTypeList>("BuildingTypeList")`

# Resource Generator

- timer
- timerMax

# Resource Nodes

- &check; Criar o WoodResourceNode utilizando o trunk com sprite
- &check; Criar script para garantir o Sorting Order
  - SortingOrder -(int) transform.position.y
  - utilizar precissonMultiplier = 5f, seria no caso ter 5 SortingOrder para cada position no jogo
- _ No ResourceGenerator implementar a lógica de buscar os resource nodes utilizando physics2d
  - Adicionar um collider as árvores
  - OverlapCircleAll retorna uma lista que deve ser percorrida para saber quais os resource nodes foram colididos.