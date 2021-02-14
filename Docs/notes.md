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
- &check; No ResourceGenerator implementar a lógica de buscar os resource nodes utilizando physics2d
  - Adicionar um collider as árvores
  - OverlapCircleAll retorna uma lista que deve ser percorrida para saber quais os resource nodes foram colididos.
- &check; Configurar a criação de recursos
  - Max Radius (detection radius)
  - Máximo número de recursos
- &check; Criar lógica para calcular o número de recursos gerados baseados no número de nós de recursos no alcance da construção
  - O número de recursos por segundo é por cada recurso no alcance, assim se a quantidade de nós de recurso é 5 e a construção gera 2 recursos por segundo, então serão 10 de recursos por segundo
- &check; Configurar os outros recursos para serem coletados

# Building placement rules

- &check; Adicionar box collider em cada harvester
- &check; Building Manager implementará a lógica para garantir a construção
  - OverlapBoxAll
  - Não construir embaixo de cada construção
  - Não construir próximo a construções iguais
  - Apenas construir se existir alguma construção próxima
- &check; Adicionar Debug circle para a regra de construções iguais próximas
- &check; Adicionar regra para construir coletadores próximos de outras construções
- &check; Criar o building HQ
  - Será a construção inicial, sem ela não será possível construir nenhuma outra

# Building animations

- &check; Criar animações para o wood harvester

# Resource Generator Overlay

- &check; Criar o objeto
  - Esse objeto está no Top Sorting Layer
  - backgroundOutline
  - bar -> bar
    - essa barra será redimensionada, o pivot da barra deve ser colocado na esquerda
  - Text
  - ícone do recurso
- &check; Criar Script para definir o comportamento
  - _ Adicionar o sprite de cada building
  - _ Adicionar o tamanho da barra para diminutir 
  - _ fazer a barra aumentar
- &check; O Resource generador overlay será adicionado nos prefabs de cada harvester e HQ
- &check; Criar o Resource generator overlay para o ghost exibir as informações de eficiencia da construção


# Criar o custo de criação das edificações

- &check; Criar uma classe de ResourceAmount e uma lista no BuildingSO para cada recurso necessário

# Tooltip para informação do jogador

- &check; TooltipUI
  - preto com texto
- &check; TooltipUI Script
- TExtMeshPro.setText
- ForceMeshUpdate
- &check; Alterar o tamanho do background utilizando GetRenderedValues e atualizando o transform do background.sizeDelta
- &check; Adiciar um padding para o background
- &check; O tooltip deve ser renderizado no mousePosition
  - utilizar o Canvas Scale para corrigir a posição do tooltip (11 minutos)
- &check; Garantir que o tooltip não ultrapasse a tela
- &check; Mostrar o tooltip por eventos
  - Implementar OnMouseEnterExit para garantir eventos quando o mouse está sobre o objeto ou não
- &check; Desabilitar Raycast Target
- &check; Adicionar um cor no textos

# Health system


