CREATE TABLE [dbo].[Members] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (100) NOT NULL,
    [LastName]  NVARCHAR (100) NOT NULL,
    [Title]     NVARCHAR (150) NULL,
    [Email]     NVARCHAR (255) NOT NULL,
    [Password]  NVARCHAR (255) NULL,
    [CompanyId] INT            NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Members_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Members_CompanyId]
    ON [dbo].[Members]([CompanyId] ASC);

