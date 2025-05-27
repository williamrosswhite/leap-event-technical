# Design for a High-Traffic Ticketing System with Caching

A ticketing system for a high-traffic event platform must handle thousands of concurrent users buying tickets. To ensure scalability, fault tolerance, and high availability, the system can leverage cloud services and distributed caching.

### Key Elements

##### Frontend
- A React-based Single Page Application (SPA) hosted on Azure Static Web Apps for scalability and global distribution.
	- The static front end is not manipulated by user and only makes a series of API calls to the Azure App Service back end for ticket purchasing and event data.
##### Load Balancer
- Azure Front Door for global load balancing and DDoS protection.
	- Azure Front Door routes traffic to nearest backend based on latency and supports multiple routing prioritization methods
	- Provides built-in caching for static content, Azure DDoS protection, Health probes, and protection from things like SQL injection and cross-site scripting.
	- Routes around downed backends to keep service up.
  - Alternatively we could manually customize our own load balancing of resources in Azure to suit our needs.
##### Backend
- Built with ASP.NET Core, hosted on Azure App Service.
- Implements RESTful APIs for ticket purchasing, event management, and user authentication.
- Uses Azure API Management for rate limiting and security.
##### Database
 - Azure SQL Database for write heavy, secure data like user accounts, auth data, ticket purchase transactions, and other relational data with strict consistency requirements.
 - Azure Cosmos DB for read heavy, distributed, high access speed data like event metadata with cloud native NoSQL format data structure. 
	 - Allows replication of data across multiple regions, providing low-latency access worldwide through automated routing, supporting both manual and automatic scaling.
##### Caching
- Azure Cache for Redis to reduce database load and improve response times.
	- Caches frequently accessed data like event details, ticket availability, and user session data.
	- Large data sets can be scaled horizontally by splitting data up into multiple nodes which each handle subsets of data.
		- Automatically handles cache workers to keep data in sync.
	- Great for applications like a ticket service which primarily need to have a lot of data quickly available to be accessed
	- There is of course some risk in having all of one's eggs in the same Azure basket and relying on Azure to handle so many things for us, but everything is configurable, and there are also benefits to tight integration between services under the same cloud provider.
		- Alternatively, we could manually deploy customized Redis containers.
##### Queueing
- Azure Service Bus is a message queuing service which can be leveraged for asynchronously handling spikes in traffic for processing ticket purchases asynchronously to handle spikes in traffic.
	- I'm more familiar with RabbitMQ for more directly controlled queueing of messages between microservices, but Azure Service Bus presumably has the same functionality and additional integration benefits of being under the umbrella of Azure cloud along with everything else.
<br><br>

---

### Architecture Diagram
```mermaid
graph TD
    A[Users] -->|HTTP Requests| B[Azure Front Door]
    B -->C[Azure Static Web Apps]
    B -->D[Azure App Service Backend APIs]
    D <-->|Secure SQL| E[Azure SQL Database]
    D <-->|Distributed NoSQL| F[Azure Cosmos DB]
    D <-->|Cache| G[Azure Cache for Redis]
    G -->|Cached Data| D
    D <-->|Queue Messages| H[Azure Service Bus]
    H <-->|Process and Update| E[Azure SQL Database]
    C -->|API Calls| D
    D -->|API Responses| C
