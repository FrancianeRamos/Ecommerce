
---

# 🏪 Sistema de E-commerce com Design Patterns

## 🎯 Introdução

Este projeto foi desenvolvido para reforçar o conhecimento em **Design Patterns** aplicados a um sistema de **e-commerce**. Ele permite o gerenciamento de pedidos com um fluxo bem definido entre diferentes estados, além da aplicação de estratégias de cálculo de frete.

### 🚀 Funcionalidades principais

1. **Gerenciamento de Estados do Pedido**: Os pedidos transitam entre **Pendente, Aprovado, Cancelado e Processando**, garantindo um controle preciso do fluxo de compra.
2. **Cálculo de Frete Inteligente**: O sistema oferece métodos de envio **terrestre (caminhão)** e **aéreo (avião)**, aplicando tarifas de 5% e 10%, respectivamente.
3. **Cancelamento Definitivo**: Após um pedido ser cancelado, ele não pode mais ser modificado.
4. **Flexibilidade para Novas Estratégias de Envio**: A estrutura permite adicionar novos métodos de envio sem comprometer a arquitetura existente.

## 🛠️ Padrões de Design Utilizados

### 🔁 **State**
O padrão **State** gerencia a transição entre os estados do pedido (**Pendente, Aprovado, Cancelado e Processando**), garantindo que cada etapa siga regras de negócio específicas.

### 🎛️ **Strategy**
O padrão **Strategy** define diferentes métodos de cálculo de frete, permitindo a extensão do sistema sem a necessidade de alterar código já existente.

## 🏗️ Estrutura do Projeto

O sistema foi desenvolvido como uma **Web API** utilizando **ASP.NET Core** e banco de dados **PostgreSQL**, garantindo escalabilidade e eficiência.

### 📂 1. **Repository**
A camada **Repository** gerencia o acesso ao banco de dados, isolando as operações e garantindo segurança na manipulação dos dados.


### ⚙️ 2. **Service**
Na camada **Service**, estão implementadas as regras de negócio, incluindo a transição de estados e o cálculo do frete via **Strategy**.

#### 📌 PedidoService:
- Controla a lógica de estados do pedido utilizando **State**.
- Executa o cálculo de frete conforme as regras de **Strategy**.
- Impede mudanças indevidas em pedidos que foram cancelados.

### 🎮 3. **Controller**
A camada **Controller** atua como intermediária entre as requisições do usuário e a lógica do sistema, garantindo que os dados sejam processados corretamente.

---

