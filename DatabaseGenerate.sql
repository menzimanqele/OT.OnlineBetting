--TODO Migration using Fluent Migrations 


-- ======================================
-- Ensure Database Exists
-- ======================================
IF NOT EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = N'online_betting'
)
BEGIN
    CREATE DATABASE online_betting;
END


USE online_betting;


-- Drop tables if they already exist
DROP TABLE IF EXISTS Wager, Account, Player, Game, Provider, Country, TransactionType;

-- Country
CREATE TABLE Country (
                         Id UNIQUEIDENTIFIER PRIMARY KEY,
                         Code VARCHAR(10) NOT NULL
);

-- Player
CREATE TABLE Player (
                        Id UNIQUEIDENTIFIER PRIMARY KEY,
                        UserName VARCHAR(100) NOT NULL,
                        CountryId UNIQUEIDENTIFIER NOT NULL,
                        FOREIGN KEY (CountryId) REFERENCES Country(Id)
);

-- Account
CREATE TABLE Account (
                         Id UNIQUEIDENTIFIER PRIMARY KEY,
                         PlayerId UNIQUEIDENTIFIER NOT NULL,
                         CreatedBy VARCHAR(100),
                         DateCreated DATETIME2 NOT NULL,
                         DateUpdated DATETIME2,
                         UpdatedBy VARCHAR(100),
                         FOREIGN KEY (PlayerId) REFERENCES Player(Id)
);

-- Provider
CREATE TABLE Provider (
                          Id UNIQUEIDENTIFIER PRIMARY KEY,
                          Name VARCHAR(100) NOT NULL,
                          IsActive BIT NOT NULL
);

-- Game
CREATE TABLE Game (
                      Id UNIQUEIDENTIFIER PRIMARY KEY,
                      Name VARCHAR(100) NOT NULL,
                      ProviderId UNIQUEIDENTIFIER NOT NULL,
                      IsActive BIT NOT NULL,
                      FOREIGN KEY (ProviderId) REFERENCES Provider(Id)
);

-- TransactionType
CREATE TABLE TransactionType (
                                 Id UNIQUEIDENTIFIER PRIMARY KEY,
                                 Name VARCHAR(50) NOT NULL
);

-- Wager
CREATE TABLE Wager (
                       Id UNIQUEIDENTIFIER PRIMARY KEY,
                       GameId UNIQUEIDENTIFIER NOT NULL,
                       TransactionId UNIQUEIDENTIFIER NOT NULL UNIQUE,
                       BrandId UNIQUEIDENTIFIER NOT NULL,
                       PlayerId UNIQUEIDENTIFIER NOT NULL,
                       ExternalReferenceId UNIQUEIDENTIFIER,
                       NumberOfBets INT,
                       Duration BIGINT,
                       TransactionTypeId UNIQUEIDENTIFIER NOT NULL,
                       Amount DECIMAL(18, 4),
                       CreatedBy VARCHAR(100),
                       DateCreated DATETIME2 NOT NULL,
                       DateUpdated DATETIME2,
                       UpdatedBy VARCHAR(100),
                       FOREIGN KEY (GameId) REFERENCES Game(Id),
                       FOREIGN KEY (TransactionTypeId) REFERENCES TransactionType(Id),
                       FOREIGN KEY (PlayerId) REFERENCES Player(Id)
);



-- Countries
INSERT INTO Country (Id, Code) VALUES
                                   ('11111111-0000-0000-0000-000000000001', 'ZA'),
                                   ('11111111-0000-0000-0000-000000000002', 'NG');

-- Players
INSERT INTO Player (Id, UserName, CountryId) VALUES
                                                 ('22222222-0000-0000-0000-000000000001', 'Jay.Bernhard67', '11111111-0000-0000-0000-000000000001'),
                                                 ('22222222-0000-0000-0000-000000000002', 'testUser21', '11111111-0000-0000-0000-000000000002');

-- Accounts
INSERT INTO Account (Id, PlayerId, CreatedBy, DateCreated) VALUES
                                                               ('33333333-0000-0000-0000-000000000001', '22222222-0000-0000-0000-000000000001', 'system', GETUTCDATE()),
                                                               ('33333333-0000-0000-0000-000000000002', '22222222-0000-0000-0000-000000000002', 'system', GETUTCDATE());

-- Providers
INSERT INTO Provider (Id, Name, IsActive) VALUES
    ('44444444-0000-0000-0000-000000000001', 'Ergonomic Soft Fish', 1);

-- Games
INSERT INTO Game (Id, Name, ProviderId, IsActive) VALUES
    ('55555555-0000-0000-0000-000000000001', 'Ergonomic Granite Cheese', '44444444-0000-0000-0000-000000000001', 1);

-- Transaction Types
INSERT INTO TransactionType (Id, Name) VALUES
                                           ('66666666-0000-0000-0000-000000000001', 'Bet'),
                                           ('66666666-0000-0000-0000-000000000002', 'Win');

-- Wagers
INSERT INTO Wager (
    Id, GameId, TransactionId, BrandId, PlayerId, ExternalReferenceId,
    NumberOfBets, Duration, TransactionTypeId, Amount,
    CreatedBy, DateCreated
) VALUES
    ('77777777-0000-0000-0000-000000000001', '55555555-0000-0000-0000-000000000001',
     '88888888-0000-0000-0000-000000000001', '99999999-0000-0000-0000-000000000001',
     '22222222-0000-0000-0000-000000000001', 'aaaaaaa1-0000-0000-0000-000000000001',
     3, 1827254, '66666666-0000-0000-0000-000000000001', 38273.9745, 'admin', GETUTCDATE()
    );