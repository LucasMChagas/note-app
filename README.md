# Note App  

Uma aplicação web simples desenvolvida para colocar em prática conceitos como **Clean Architecture**, princípios **SOLID**, **Domain-Driven Design**, testes e muito mais!  

## Funcionalidades  

- **Gerenciamento de Notas**:  
  - Criar, editar, deletar e visualizar notas.  
  - Cada usuário tem acesso apenas às suas próprias notas.  

- **Autenticação e Segurança**:  
  - Sistema de autenticação via **JWT**.  
  - Criptografia de senhas no banco de dados.  

- **Ativação de Conta**:  
  - Sistema de envio de e-mails para ativação de contas.  

## Tecnologias Utilizadas  

- **Backend**:  
  - ASP.NET com **Minimal APIs**.  
  - **Entity Framework** para o gerenciamento do banco de dados.  

- **Frontend**:  
  - **Blazor WebAssembly**, permitindo o compartilhamento de boa parte do código entre o backend e o frontend.  

- **Banco de Dados**:  
  - Suporte para bancos de dados relacionais com **Entity Framework**.  

##  Estrutura do Projeto  

A aplicação está estruturada da seguinte forma:  

- `NoteApp.Api`: Projeto **ASP.NET Minimal API** responsável pelas funcionalidades do backend.  
- `NoteApp.Web`: Projeto **Blazor WebAssembly** para o frontend da aplicação.  
- `NoteApp.Domain`: Contém a lógica de domínio compartilhada.  
- `NoteApp.Infra`: Configurações de banco de dados e integração com o **Entity Framework**.  
- `NoteApp.Tests`: Projeto de testes para garantir a qualidade e funcionamento do sistema.  

![Tela inicial da aplicação](https://github.com/LucasMChagas/note-app/blob/main/prints/home.png)
![Tela inicial da aplicação](https://github.com/LucasMChagas/note-app/blob/main/prints/login.png)
![Tela inicial da aplicação](https://github.com/LucasMChagas/note-app/blob/main/prints/register.png)
![Tela inicial da aplicação](https://github.com/LucasMChagas/note-app/blob/main/prints/notes.png)
![Tela inicial da aplicação](https://github.com/LucasMChagas/note-app/blob/main/prints/tela_swagger.png)
