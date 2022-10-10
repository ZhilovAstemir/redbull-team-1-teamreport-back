CREATE TABLE [dbo].[Reports] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [MemberId]        INT            NULL,
    [Morale]          INT            NOT NULL,
    [MoraleComment]   NVARCHAR (600) NULL,
    [Stress]          INT            NOT NULL,
    [StressComment]   NVARCHAR (600) NULL,
    [Workload]        INT            NOT NULL,
    [WorkloadComment] NVARCHAR (600) NULL,
    [High]            NVARCHAR (600) NULL,
    [Low]             NVARCHAR (600) NULL,
    [Else]            NVARCHAR (600) NULL,
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

