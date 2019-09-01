CREATE TABLE [dbo].[MediaFile] (
    [ID]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Url]      NVARCHAR (250)   NULL,
    [SpeechID] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_MediaFile] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_MediaFile_Speech] FOREIGN KEY ([SpeechID]) REFERENCES [dbo].[Speech] ([ID])
);

