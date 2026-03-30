# Socialize 🌐
### A Scalable Social Media Web API built with .NET 8

**Socialize** is a RESTful API designed to power  social networking applications. It handles the core "social graph" logic, including user authentication, content management, and complex follower relationships using a clean, maintainable architecture.

## 🚀 Key Features

### 👤 Identity & Security
- **JWT Authentication:** Secure stateless authentication using JSON Web Tokens.
- **User Management:** Complete Registration, Login, and Profile management.
- **Role-Based Access:** Differentiated permissions for Users and Admins.

### 📱 Social Engine
- **The Social Graph:** Advanced "Follow/Unfollow" logic using self-referencing entity relationships.
- **Content CRUD:** Full Create, Read, Update, and Delete capabilities for Posts and comments.
- **Interactions:** Real-time Like and Comment functionality to drive user engagement.

### 🛠 Technical Excellence
- **Entity Framework Core:** Optimized database queries and automated migrations.
- **DTO Pattern:** Data Transfer Objects to prevent data overposting.
- **Swagger:** Interactive documentation for easy endpoint testing.


## 🚀 Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites
* **.NET 8 SDK** (or the version you are using)
* **SQL Server** (LocalDB, Express, or Docker instance)
* **EF Core Tools** (Install via terminal: `dotnet tool install --global dotnet-ef`)

### Installation
1. **Clone the repo**
   ```bash
   git clone [https://github.com/MazenSayed21/SocialMedia-WebAPI-DotNet.git](https://github.com/MazenSayed21/SocialMedia-WebAPI-DotNet.git)

2.Configure Database
Open appsettings.json and update the DefaultConnection string to point to your local SQL Server instance.  
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=Db;Trusted_Connection=True;"
}

3.Apply Database Migrations
This command will automatically create the database and all tables (Users, Posts, Followers) on your local machine.
'dotnet ef database update'

4.Run the Project


   git clone [https://github.com/MazenSayed21/SocialMedia-WebAPI-DotNet.git](https://github.com/MazenSayed21/SocialMedia-WebAPI-DotNet.git)
