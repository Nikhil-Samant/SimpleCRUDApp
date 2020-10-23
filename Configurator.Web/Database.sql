Create Database Configurator;

Use Configurator;

Create table Configuration (
    Id INT IDENTITY(1,1),
    ConfigurationName NVARCHAR(50),
    ConfigType NVARCHAR(20),
    Config NVARCHAR(MAX),
    CONSTRAINT PK_Configuration PRIMARY KEY (Id)
)