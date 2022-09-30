CREATE TABLE [dbo].[Reports] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [MemberId]        INT            NULL,
    [Morale]          INT            NOT NULL,
    [MoraleComment]   NVARCHAR (MAX) NULL,
    [Stress]          INT            NOT NULL,
    [StressComment]   NVARCHAR (MAX) NULL,
    [Workload]        INT            NOT NULL,
    [WorkloadComment] NVARCHAR (MAX) NULL,
    [High]            NVARCHAR (MAX) NULL,
    [Low]             NVARCHAR (MAX) NULL,
    [Else]            NVARCHAR (MAX) NULL,
    [WeekId]          INT            NULL,
    CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reports_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id]),
    CONSTRAINT [FK_Reports_Weeks_WeekId] FOREIGN KEY ([WeekId]) REFERENCES [dbo].[Weeks] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Reports_WeekId]
    ON [dbo].[Reports]([WeekId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Reports_MemberId]
    ON [dbo].[Reports]([MemberId] ASC);

