# Gestão de Produtos API (CRUD) - Projeto de Testes/Estudo

## Projeto Gestão de Produtos API (CRUD)
Implementando na prática Rest API (CRUD) com conceitos de DDD + .NET CORE + IoC + Tests

## Camadas
Primeiramente iremos realizar a organização por pastas que representará as camadas conforme no DDD, que serão:

Presentation (Apresentação)
Application (Aplicação)
Domain (Domínio)
Infrastructure (Infraestrutura)
Tests (Testes)

#### Presentation Layer:
A Camada de Apresentação é responsável por lidar com a interface e interação do usuário. Inclui componentes como APIs da web, interfaces de usuário ou interfaces de linha de comando. Esta camada se preocupa principalmente em receber entradas do usuário, exibir informações e coordenar o fluxo de dados entre o usuário e a aplicação.
#### Application Layer:
A camada de aplicativo contém a lógica de negócios e os casos de uso específicos do aplicativo. Ele atua como intermediário entre a camada de apresentação e a camada de domínio. Esta camada orquestra a execução de casos de uso, coordenando as interações entre diferentes componentes e aplicando regras de negócios. Ele não contém detalhes relacionados à infraestrutura ou específicos à implementação.
#### Domain Layer:
A camada de domínio encapsula a lógica de negócios central e as entidades do aplicativo. Ele representa o coração do sistema e contém regras de negócios, entidades, objetos de valor e lógica específica de domínio. A Camada de Domínio é independente de quaisquer estruturas ou tecnologias externas e deve ser a parte mais estável e reutilizável da arquitetura.
#### Infrastructure Layer:
A camada de infraestrutura lida com questões externas e fornece implementações para acesso a dados, serviços externos, estruturas e outros códigos relacionados à infraestrutura. Inclui componentes como bancos de dados, sistemas de arquivos, APIs de terceiros e integrações externas. A camada de infraestrutura interage com o mundo externo, permitindo que o aplicativo armazene e recupere dados, comunique-se com sistemas externos e lide com questões transversais, como registro em log ou cache.


## Architecture Tests
Os testes de arquitetura, também conhecidos como testes de arquitetura ou testes estruturais, são um tipo de teste que se concentra na verificação da aderência de um sistema de software ao projeto arquitetônico pretendido. Esses testes examinam os relacionamentos e dependências entre vários componentes, módulos, camadas ou projetos dentro da arquitetura.

Os principais objetivos dos testes de arquitetura são:
1. **Aplicando Design e Estrutura**: Os testes de arquitetura validam se o sistema de software segue o design arquitetônico pretendido, garantindo a separação adequada de preocupações e interações de componentes.
2. **Detecção de violações arquitetônicas**: Os testes de arquitetura identificam desvios das restrições arquiteturais esperadas, como acoplamento rígido ou desvio de camadas, permitindo a detecção precoce e a resolução de problemas arquitetônicos.
3. **Prevenção de regressões**: Ao incluir testes de arquitetura no conjunto de testes, as equipes podem detectar alterações não intencionais que podem impactar a arquitetura, evitando possíveis regressões.
4. **Melhorando a capacidade de manutenção e a flexibilidade**: Os testes de arquitetura promovem a capacidade de manutenção do código, a modularidade e a facilidade de fazer alterações na arquitetura, melhorando a capacidade de manutenção e a flexibilidade do sistema a longo prazo.
5. **Facilitando a colaboração**: Os testes de arquitetura servem como uma ferramenta de comunicação, fornecendo uma compreensão compartilhada da arquitetura, dos padrões e das dependências do sistema entre os membros da equipe e as partes interessadas.

O projeto fornecido demonstra a presença de testes de arquitetura que impõem princípios de arquitetura limpa. Esses testes validam as dependências entre diferentes camadas ou projetos, garantindo que as camadas Domínio, Aplicação, Infraestrutura e Apresentação aderem às dependências pretendidas e não violam os princípios da arquitetura limpa. Ao executar esses testes de arquitetura, o projeto mantém uma estrutura consistente e modular, apoiando a capacidade de manutenção, testabilidade e flexibilidade que a arquitetura limpa promove.

## Attributions
* Clean Architecture: A Craftsman's Guide to Software Structure and Design by Robert C. Martin
* https://www.milanjovanovic.tech/blog/clean-architecture-folder-structure
* https://www.milanjovanovic.tech/blog/clean-architecture-and-the-benefits-of-structured-software-design
* https://www.milanjovanovic.tech/blog/enforcing-software-architecture-with-architecture-tests