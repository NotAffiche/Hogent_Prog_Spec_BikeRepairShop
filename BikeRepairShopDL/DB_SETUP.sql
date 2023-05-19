CREATE TABLE [dbo].[Customer] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    VARCHAR (500) NOT NULL,
    [Email]   VARCHAR (500) NOT NULL,
    [Address] VARCHAR (500) NOT NULL,
    [Status]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Bike] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [BikeType]     VARCHAR (50)  NOT NULL,
    [Description]  VARCHAR (500) NULL,
    [PurchaseCost] FLOAT (53)    NOT NULL,
    [CustomerId]   INT           NOT NULL,
    [Status]       INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);

CREATE TABLE [dbo].[Repairman] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (500) NOT NULL,
    [Email]       VARCHAR (500) NOT NULL,
    [CostPerHour] FLOAT (53)    NOT NULL,
    [Status]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);