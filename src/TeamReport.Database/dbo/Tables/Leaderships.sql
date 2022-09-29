CREATE TABLE [dbo].[Leaderships] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [LeaderId] INT NULL,
    [MemberId] INT NULL,
    CONSTRAINT [PK_Leaderships] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Leaderships_Member_LeaderId] FOREIGN KEY ([LeaderId]) REFERENCES [dbo].[Member] ([Id]),
    CONSTRAINT [FK_Leaderships_Member_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Member] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Leaderships_LeaderId]
    ON [dbo].[Leaderships]([LeaderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Leaderships_MemberId]
    ON [dbo].[Leaderships]([MemberId] ASC);

