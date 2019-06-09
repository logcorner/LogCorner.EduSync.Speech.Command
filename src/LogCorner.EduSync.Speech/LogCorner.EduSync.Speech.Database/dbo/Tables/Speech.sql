CREATE TABLE [dbo].[Speech] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Title]       NVARCHAR (250)   NOT NULL,
    [Description] NVARCHAR (MAX)   NOT NULL,
    [Url]         NVARCHAR (250)   NOT NULL,
    [Type]        INT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Presentation] PRIMARY KEY CLUSTERED ([ID] ASC)
);



