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


-- ======================================
-- DROP EXISTING TABLES (for dev only)
-- ======================================
IF OBJECT_ID('dbo.Wager', 'U') IS NOT NULL DROP TABLE dbo.Wager;
IF OBJECT_ID('dbo.Game', 'U') IS NOT NULL DROP TABLE dbo.Game;
IF OBJECT_ID('dbo.Player', 'U') IS NOT NULL DROP TABLE dbo.Player;
IF OBJECT_ID('dbo.Provider', 'U') IS NOT NULL DROP TABLE dbo.Provider;
IF OBJECT_ID('dbo.TransactionType', 'U') IS NOT NULL DROP TABLE dbo.TransactionType;
IF OBJECT_ID('dbo.Country', 'U') IS NOT NULL DROP TABLE dbo.Country;


-- ======================================
-- CREATE TABLES
-- ======================================

-- Country
CREATE TABLE Country (
                         Id UNIQUEIDENTIFIER PRIMARY KEY,
                         Code CHAR(2) NOT NULL
);


-- Provider
CREATE TABLE Provider (
                          Id UNIQUEIDENTIFIER PRIMARY KEY,
                          Name NVARCHAR(100) NOT NULL,
                          IsActive BIT NOT NULL
);


-- TransactionType
CREATE TABLE TransactionType (
                                 Id UNIQUEIDENTIFIER PRIMARY KEY,
                                 Name NVARCHAR(100) NOT NULL
);


-- Player
CREATE TABLE Player (
                        Id UNIQUEIDENTIFIER PRIMARY KEY,
                        UserName NVARCHAR(100) NOT NULL,
                        CountryId UNIQUEIDENTIFIER NOT NULL,
                        FOREIGN KEY (CountryId) REFERENCES Country(Id)
);


-- Game
CREATE TABLE Game (
                      Id UNIQUEIDENTIFIER PRIMARY KEY,
                      Name NVARCHAR(100) NOT NULL,
                      ProviderId UNIQUEIDENTIFIER NOT NULL,
                      IsActive BIT NOT NULL,
                      FOREIGN KEY (ProviderId) REFERENCES Provider(Id)
);


-- Wager
CREATE TABLE Wager (
                       Id UNIQUEIDENTIFIER PRIMARY KEY,
                       GameId UNIQUEIDENTIFIER NOT NULL,
                       TransactionId UNIQUEIDENTIFIER NOT NULL,
                       BrandId UNIQUEIDENTIFIER NOT NULL,
                       UserId UNIQUEIDENTIFIER NOT NULL,
                       ExternalReferenceId UNIQUEIDENTIFIER NOT NULL,
                       NumberOfBets INT NOT NULL,
                       Duration BIGINT NOT NULL,
                       TransactionTypeId UNIQUEIDENTIFIER NOT NULL,
                       Amount DECIMAL(18, 2) NOT NULL,
                       CreatedBy NVARCHAR(100) NOT NULL,
                       DateCreated DATETIME2 NOT NULL,
                       DateUpdated DATETIME2 NULL,
                       UpdatedBy NVARCHAR(100) NULL,

                       CONSTRAINT FK_Wager_Game FOREIGN KEY (GameId) REFERENCES Game(Id),
                       CONSTRAINT FK_Wager_Player FOREIGN KEY (UserId) REFERENCES Player(Id),
                       CONSTRAINT FK_Wager_TransactionType FOREIGN KEY (TransactionTypeId) REFERENCES TransactionType(Id),

                       CONSTRAINT UQ_Wager_TransactionId UNIQUE (TransactionId)
);


-- Index for filtering player wagers
CREATE INDEX IX_Wager_UserId_CreatedDate ON Wager(UserId, DateCreated DESC);


-- ======================================
-- SEED DATA
-- ======================================

-- Countries
INSERT INTO Country (Id, Code)
VALUES
    ('a5f4f580-5c94-4a56-b7ef-1d97d531d0cd', 'ZA'),
    ('c81d41aa-e542-4f1b-8883-bdfd0f2768d5', 'BS');


-- Providers
INSERT INTO Provider (Id, Name, IsActive)
VALUES
    ('e4e6e4ac-fd4f-4b32-9fdd-b0679a6200fd', 'Ernomic Soft Fish', 1),
    ('ea527a0c-0f3f-46a2-b2e4-1e3229eb4a3a', 'Test Provider', 1);


-- Transaction Types
INSERT INTO TransactionType (Id, Name)
VALUES
    ('cc39c9b3-3b4f-4a69-b222-0bb153dfed1a', 'Bet'),
    ('5ecad2f2-7a42-4b6e-a7f0-c8e8ab1f9af2', 'Win'),
    ('4d75b478-9cf2-4937-b0b4-eae6b3e4b145', 'Refund');

