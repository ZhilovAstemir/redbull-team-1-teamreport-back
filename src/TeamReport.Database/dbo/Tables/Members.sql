CREATE TABLE [dbo].[Members] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (MAX) NOT NULL,
    [LastName]  NVARCHAR (MAX) NOT NULL,
    [Title]     NVARCHAR (MAX) NULL,
    [Email]     NVARCHAR (MAX) NOT NULL,
    [Password]  NVARCHAR (MAX) NOT NULL,
    [CompanyId] INT            NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Members_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Members_CompanyId]
    ON [dbo].[Members]([CompanyId] ASC);

