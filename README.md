# Tomb of the Mask - Clone VB.NET

Clone fiel do jogo **Tomb of the Mask** desenvolvido em Visual Basic .NET (Windows Forms).

Este projeto inclui: movimento com rastro, armadilhas animadas, física própria, sistema de escudos temporários e interface polida.

## Funcionalidades

- **Mecânica principal**: Movimento em grelha com rastro (tail) que se desfaz
- **Armadilhas animadas**:
  - Bats voadores
  - Dart traps com projéteis
  - Snakes com alerta e perseguição
  - Plataformas temporárias com sequência de destruição
  - Hidden spikes
  - Straw traps
  - Portais de teletransporte
- Sistema de **escudos** (altera visual do player por 10 segundos)
- Colecionáveis: Stars (estrelas), Dots e Coins
- Contagem decrescente no início de cada nível + animação de fade
- Pause menu
- Sistema de progresso (estrelas por nível)
- Loja e modo Speed Run (desafio de tempo)

## Estrutura do Projeto

- `Lvl1.vb`, `Lvl2.vb` ... `Lvl10.vb` → Níveis individuais
- Formulários de Menu, Loja, Speed Run Mode, etc.
- Pasta `imgM\` com todas as animações e sprites
- Pasta `sounds\` com efeitos sonoros

## Como Executar

1. Abrir a solução no **Visual Studio 2022**
2. Compilar em Release ou Debug
3. Executar (recomendado em ecrã maximizado)

**Requisitos:**
- .NET Framework 4.8 ou superior
- Todas as imagens e sons na pasta `imgM`
- Resolução full HD com escala de 125%

## Controles

- **WASD** ou **Setas** → Movimento
- **ESC** → Pausa
- **Double Click** → Ativar escudo (se disponível)

## Limitações conhecidas

- Muitos timers independentes.
- Alguma lógica duplicada entre níveis.
- Dependente de ficheiros externos (imagens/sons) — fácil dar erro de path.

---
