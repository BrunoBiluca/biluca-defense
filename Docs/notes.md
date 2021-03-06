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

# Enemies

- &check; Exibir a barra de vida apenas quando esta for menor que 1
- &check; Criar o prefab do enemy, um círculo
- &check; Adicionar script para movimento do inimigo, linha reta em direção a construção
- &check; Colisão do enemy
- &check; Criação do enemy
- &check; Implementar a procura por construções
- &check; Implementar um timer para a procura por construções
  - Adicionar um timer random no início assim cada um dos inimigos vão buscar em frames diferentes
- &check; Adicionar os resources nodes como Trigger
- &check; Adicionar a animação do enemy

# Tower

- &check; Adicionar um valor inicial de recursos
- &check; Criar prefab da Torre
  - Basear no prefab do HQ
  - Colocar um ponto para o spawn dos projéteis
- &check; Look for targets pode ser transormado em um script de foundation
- &check; Adicionar look for targets na Torre
- &check; Criar o Arrow
  - Rigidbody
  - Box collider trigger
- &check; Criar script para o Arrow
  - Deve chegar ao inimigo
- &check; Adicionar um timer para os tiros (ShootPerSecond)
- &check; Resolver problema com Arrow que não está andando no mapa
- &check; Converter o moveDir para EulerAngles
  - Mathf.Atan2(y, x) * Rad2Deg
- &check; Adicionar o continuamento dos arrows 
- &check; Adicionar arrows para o HQ também
- &check; Corrigir bug quando para de coletar recursos a UI para de atualizar
- &check; Adicionar tempo de vida para o Arrow

## Wave Manager

- &check; Criar um gerenciador de spawn dos inimigos
- &check; Instanciar vários inimigos por vez
- &check; Instanciar inimigos um após o outro
- &check; Implementar o sistema de ondas utilizando StatePattern
- &check; Aumentar o número de inimigos a cada onda
- &check; Mudar o lugar que os inimigos são invocados por um raio do HQ
- &check; Criar um indicador para mostrar a posição que os inimigos foram invocados
  - Um círculo vermelho mostrando a próxima onda
- &check; Adicionar um texto para mostrar o número da onda
- &check; Adicionar um texto para mostrar o tempo até a próxima onda
  - &check; Criar um script para renderizar essa UI
- &check; Adicionar um arrow para mostrar o local que serão invocados os inimigos nas bordas da tela
  - Remover o indicador quando o círculo da wave já está na tela
  - Calcular a posição com a câmera principal tem que ser maior que o OrthographicSize * 1.5f
- &check; Criar o indicador para o ínimigo mais próximo

# Shaders

- _ SpriteEmission
  - Material
  - Shader
- _ Adicionar _MainTex
- Adicionar _EmissionColor
- Criar para ouro
- Criar para inimigos
- Criar para o arrow com a máscara

# Building Construction

- &check; Construir um objeto placeholder enquanto a construção está sendo instanciada
- _ Adicionar o tempo de construção para cada construção
- &check; Utilizar as informações do BuildingType para formar o placeholder
  - Collider
- &check; Adicionar uma barra para mostrar o processo
  - Criar canvas image com uma máscara menor
  - Girar no sentido horário com a rotação começando de cima
- _ Corrigir problema com instanciar várias construções enquanto estão sendo construidas
- &check; Adicionar o shader

# Demolish building

- Criar o botão de demolição
- Adicionar esse botão em cada building
- Fazer o player recuperar um pouco dos recursos
- Fazer o botão ser visível apenas no mouseover
  - OnMouseEnter, OnMouseExit