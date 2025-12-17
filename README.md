
<img src="EspacoTech/wwwroot/src/logoMain.png" alt="logo">

## by Rodolfo Melo

O **EspacoTech** é uma solução web desenvolvida em **ASP.NET MVC** focada na gestão inteligente de reservas e espaços. O sistema foi projetado para oferecer uma interface amigável e segura, permitindo que usuários autenticados gerenciem agendamentos de forma eficiente.

---

## 📝 Resumo do Projeto

Ele utiliza o padrão de arquitetura **MVC (Model-View-Controller)** para separar as responsabilidades de lógica de negócio, persistência de dados e interface do usuário, garantindo uma aplicação escalável e de fácil manutenção.

---

## 🛠️ Tecnologias Utilizadas

### **Back-end & Banco de Dados**
* **Framework:** .NET 8
* **ORM:** Entity Framework Core (EF Core)
* **Banco de Dados:** Microsoft SQL Server 2022
* **Bibliotecas Adicionais:**
    * `EntityFrameWorkCore.SqlServer` & `Design`
    * `EntityFrameWorkCore.Tools` (Gerenciamento de Migrations)
    * `Microsoft.VisualStudio.Web.CodeGeneration.Design` (Geração de Scaffolding)

### **Front-end**
* **Linguagens:** HTML5, CSS3, JavaScript
* **Design:** Layout Responsivo (adaptável a dispositivos móveis e desktop)

---

## ⚙️ Funcionalidades

* **CRUD Completo:** Gerenciamento de Salas e Reservas (Criar, Visualizar, Editar e Excluir).
* **Sistema de Identidade:** Cadastro e Login de usuários.
* **Proteção de Rotas:** Filtros de autorização que garantem que apenas usuários autenticados acessem áreas críticas de cadastro.

---

## 🚀 Passo a Passo para Rodar o Projeto

Siga as etapas abaixo para configurar o ambiente em sua máquina local:

### 1. Clonagem
Clone o repositório para o seu diretório local:
```bash
git clone [https://github.com/dolfo-melo/espacotech.git](https://github.com/username/espacotech.git)
```

### 2. Abrir sua IDE de Desenvolvimento
Localize o arquivo appsettings.json na raiz do projeto e atualize a chave DefaultConnection com as credenciais do seu SQL Server:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=SUA_INSTANCIA;Database=EspacoTechDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
```

### 4. Remova a pasta "Migrations"

### 5. Adicionar Migração e Atualizar Banco de Dados
Abra o Console do Gerenciador de Pacotes (Package Manager Console) e execute:
```bash
    # Adiciona uma nova migração
    Add-Migration InitialTable

    # Atualiza o Banco de Dados
```