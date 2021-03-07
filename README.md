# biluca-defense
Build a awesome Tower Defense game, with a lot of systems and effects

# Dia 2

- Criação de scriptable objects para criação de construções dinamicamente
  - BuildingSO
  - BuildingFactorySO
- Mapeamento de atalhos (shortcuts) para alteração do tipo a ser construído
- Melhoria na estrutura do projeto

![](Docs/scriptable_objects_and_shortcuts.PNG)

# Dia 4

- Criação do sistema de gerenciamento de recursos
- Criação do sistema de movimentação de câmera
  - Utilizando as teclas ```up, down, left, right```
- UI para selecionar as construções

![](Docs/selected_buttons.PNG)

# Dia 5

- Criação do fantasma das construções para dar um preview ao jogador de como a construção estará no mundo

![](Docs/building_ghost.PNG)

# Dia 6

- Criação do nodos de recursos
- Implementação do sistema para buscar recursos próximos as construções

![](Docs/resource_nodes.PNG)

# Dia 7

- Criação dos outros nodos de recursos
- Configuração de geração de recursos para todos os nodos

![](Docs/all_resource_nodes.PNG)

# Dia 8

- Criação da edificação de HQ, principal estrutura do jogo
- Criação das regras de construção das edificações

![](Docs/hq.PNG)

# Dia 9

- Criação do overlay para exibir a quantidade de recursos coletados, melhorando assim a visualização para o usuário
- Criação do overlay para exibir a eficiência de uma edificação ser construída naquele lugar

![](Docs/resource_overlay.PNG)

# Dia 10-11

- Criação de sistema de tooltip para exibir mensagens para o usuário
- Criação de sistema de vida para as edificações

![](Docs/health_system.PNG)

# Dia 12

- Criação dos inimigo
- Criação de invocação do inimigo

![](Docs/enemy_spawner.PNG)

# Dia 13-16

- Criação do sistema de ondas dos inimigos
- Várias refatorações no código para desacoplar várias mecânicas que podem ser utilizadas em outros projeto

![](Docs/enemy_wave_system.PNG)

- Adição de Post Processing no jogo para ficar mais legal

![](Docs/bloom.PNG)

# Dia 17-19

- Adição de efeito e tempo para a construção das edificações, utilizando shaders para criar um efeito legal

![](Docs/building_constructor.PNG)

# Dia 20

- Adição do botão para destruir as edificações criadas e assim recuperar um pouco de recurso

![](Docs/minimap.PNG)