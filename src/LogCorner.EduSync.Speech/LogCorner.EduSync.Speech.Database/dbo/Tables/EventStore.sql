CREATE TABLE [dbo].[EventStore] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [Version]     BIGINT           NOT NULL,
    [AggregateId] UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (250)   NOT NULL,
    [TypeName]    NVARCHAR (250)   NOT NULL,
    [OccurredOn]  DATETIME         NOT NULL,
    [PayLoad]     TEXT             NOT NULL,
    [IsSync]      BIT              NOT NULL,
    CONSTRAINT [PK__EventStore] PRIMARY KEY CLUSTERED ([Id] ASC)
);

