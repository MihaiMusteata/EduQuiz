
## Descriere

EduQuiz este o platformă educațională pentru gestionarea și desfășurarea quizurilor online. Proiectul explorează două arhitecturi software principale: monolitică și bazată pe microservicii. Acesta a fost dezvoltat ca parte a unei lucrări de cercetare la Universitatea Tehnică a Moldovei (UTM), în cadrul disciplinei Arhitectura Sistemelor Software.

Repository-ul principal conține branch-uri separate pentru fiecare arhitectură:
- **Branch monolith**: Implementare monolitică – [Vezi branch-ul](https://github.com/MihaiMusteata/EduQuiz/tree/monolith)
- **Branch microservices**: Implementare bazată pe microservicii – [Vezi branch-ul](https://github.com/MihaiMusteata/EduQuiz/tree/microservices)

Proiectul demonstrează cum arhitectura influențează atributele de calitate precum disponibilitatea, deployability, eficiența energetică și altele, în contextul aplicațiilor educaționale cu trafic variabil.

## Cercetare: Platformă de Gestionare a Quizurilor Educaționale – Monolit vs Microservicii

Această secțiune include un rezumat al referatului academic realizat pentru proiect.

### Introducere

Dezvoltarea sistemelor software educaționale este legată de capacitatea arhitecturii de a răspunde cerințelor de scalabilitate, flexibilitate și viteză. Platformele de quizuri trebuie să susțină un număr mare de utilizatori simultan, să răspundă rapid și să se adapteze la noi cerințe. Arhitectura monolitică oferă simplitate inițială, dar devine dificilă la scară mare. Arhitectura bazată pe microservicii permite servicii autonome, scalare independentă și reziliență ridicată.

Problema centrală: Alegerea arhitecturii pentru dezvoltarea unei platforme de gestionarea quizurilor și desfășurarea sesiunilor online.

### Definirea Arhitecturilor

#### Arhitectura Monolitică
- Aplicație construită ca un singur artefact executabil.
- Straturi: Presentation (API), Application (logică business), Domain (entități), Infrastructure.
- Caracteristici: Unitate de deploy, simplitate inițială, comunicare internă rapidă.
- Implementare în EduQuiz: .NET 8 Web API cu PostgreSQL, toate operațiile în același proces.

#### Arhitectura Bazată pe Microservicii
- Aplicație descompusă în servicii independente.
- Fiecare serviciu: Autonom, rulează în propriul proces, comunică via REST sau message broker.
- Caracteristici: Descentralizare, scalabilitate granulară, polyglot (tehnologii diferite).
- Implementare în EduQuiz:
  - APIGateway (Ocelot pentru routing).
  - Authentication (Identity și JWT).
  - Quiz (CRUD quizuri).
  - QuizSession (websockets pentru sesiuni online).
  - AI (Python pentru generare quizuri).
- Orchestrare: Docker Compose; baze de date separate per serviciu.

### Studiu Comparativ

Comparația se bazează pe atribute de calitate (Quality Attributes).

| Atribut          | Monolit                          | Microservicii                          |
|------------------|----------------------------------|----------------------------------------|
| **Disponibilitate** | Vulnerabil la erori globale; downtime la update. | Izolare erori; rolling updates; scalare granulară. |
| **Deployability** | Deploy integral lent și riscant. | Deploy granular rapid (blue-green, canary); aliniat DevOps. |
| **Eficiența Energetică** | Consum ridicat; execuție totală chiar la trafic redus. | Optimizare resurse; scalare doar pentru servicii solicitate. |
| **Integrabilitate** | Dificilă integrarea noilor tehnologii. | Ușoară prin servicii independente (e.g., AI în Python). |
| **Modificabilitate** | Modificări afectează întregul sistem. | Modificări izolate per serviciu. |
| **Performanță** | Bună intern, dar scalare verticală limitată. | Optimizare per serviciu; latență rețea gestionabilă. |
| **Siguranță** | Risc ridicat de erori propagate. | Izolare erori; reziliență ridicată. |
| **Securitate** | Gestionare centralizată, dar vulnerabilă. | Securitate per serviciu (JWT, API Gateway). |
| **Testabilitate** | Teste integrale complexe. | Teste unitare și integrate per serviciu. |
| **Utilizabilitate** | Simplitate inițială pentru echipe mici. | Complexitate operațională, dar flexibilitate pentru echipe mari. |

#### Avantaje și Dezavantaje
- **Monolit**: Avantaje – Dezvoltare rapidă inițială, simplitate. Dezavantaje – Scalabilitate limitată, mentenanță dificilă.
- **Microservicii**: Avantaje – Scalare independentă, reziliență, polyglot. Dezavantaje – Complexitate operațională, latență rețea.

### Concept Arhitectural și Implementare
- **Monolit**: Structură stratificată în .NET; deploy unic.
- **Microservicii**: Extinderea monolitului prin integrarea unui microserviciu de AI scris în Python; Servicii containerizate; comunicare REST; 

### Concluzii
Monolitul asigură o dezvoltare rapidă și o mentenanță simplificată în primele etape, microserviciile oferă un potențial mult mai ridicat pentru evoluția ulterioară, integrarea unor module externe și suportul unui număr mare de utilizatori. Concluzia generală este că niciuna dintre arhitecturi nu reprezintă o soluție universală, iar succesul unui proiect depinde de echilibrarea corectă a avantajelor și limitărilor fiecăreia. În final, cercetarea realizată a confirmat importanța unei arhitecturi bine alese pentru stabilitatea, performanța și scalabilitatea produsului final.

#
Autor: Musteața Mihail, TI-251M, UTM, FCIM - 2025.
