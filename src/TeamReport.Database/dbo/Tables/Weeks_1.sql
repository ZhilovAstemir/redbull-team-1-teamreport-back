CREATE TABLE [dbo].[Weeks] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [DateStart] DATETIME2 (7) NOT NULL,
    [DateEnd]   DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_Weeks] PRIMARY KEY CLUSTERED ([Id] ASC)
);

