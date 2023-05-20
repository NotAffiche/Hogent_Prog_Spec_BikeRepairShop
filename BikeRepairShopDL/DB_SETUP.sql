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

CREATE TABLE [dbo].[RepairTask] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Description]   VARCHAR (500) NOT NULL,
    [RepairTime]    INT           NOT NULL,
    [CostMaterials] FLOAT (53)    NOT NULL,
    [Status]        INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace inner tube',30,5,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('adjust gears',30,0,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('repair bicycle light',30,10,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace bicycle brakes',30,8,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace outer tube',30,10,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace chain regular bike',60,50,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace chain race bike',60,250,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace chain mountain bike',60,250,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace crankset',90,200,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('tighten spokes / align wheel',30,0,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace luggage carrier',30,35,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('adjust disc brakes',30,0,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace brake cable',30,5,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace gear cable',30,5,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('maintenance service',45,0,1);
INSERT INTO RepairTask(Description,RepairTime,CostMaterials,status) output INSERTED.ID VALUES('replace pedals',15,15,1);

CREATE TABLE [dbo].[RepairOrder] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Urgency]       VARCHAR (50)  NOT NULL,
    [OrderDate]     DATE          NOT NULL,
	[CustomerId]    INT           NOT NULL,
	[Paid]          INT           NOT NULL,
    [Status]        INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);

CREATE TABLE [dbo].[RepairOrderItem] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
	[RepairOrderId] INT           NOT NULL,
    [BikeId]        INT           NOT NULL,
	[RepairTaskId]  INT           NOT NULL,
	[RepairmanId]   INT           NOT NULL,
    [Status]        INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	FOREIGN KEY ([RepairOrderId]) REFERENCES [dbo].[RepairOrder] ([Id]),
	FOREIGN KEY ([BikeId]) REFERENCES [dbo].[Bike] ([Id]),
	FOREIGN KEY ([RepairTaskId]) REFERENCES [dbo].[RepairTask] ([Id]),
	FOREIGN KEY ([RepairmanId]) REFERENCES [dbo].[Repairman] ([Id])
);